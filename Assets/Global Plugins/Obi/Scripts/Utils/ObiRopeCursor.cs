using System;
using UnityEngine;

namespace Obi
{

	/**
	 * A rope cursor takes a ObiRope as parameter, and allows you to change its lenght by either adding or removing
	 * particles and constraints at a certain point in the rope, in either direction.
	 */
	[RequireComponent(typeof(ObiRope))]
	public class ObiRopeCursor : MonoBehaviour
	{
		[Range(0,1)]
		public float normalizedCoord; 	/**< Normalized coordinate of the spawner along the rope lenght. 0 is at the start, 1 at the end.*/

		public bool direction = true;	/**< Direction of extrusion.*/

		private ObiRope rope;

		public void Awake(){
			rope = GetComponent<ObiRope>();
		}

		/**
		 * From a given constraint index and direction, finds the constraint where rope extension/shortening will happen.
		 */
		private int FindHotConstraint(ObiDistanceConstraintBatch distanceBatch, int constraint, int maxAmount){

			// from the current particle, iterate distance constraints in the adequate direction until we find a discontinuity: 
			if (direction){

				int lastParticle = distanceBatch.springIndices[constraint*2+1];

				for (int i = 1; i <= maxAmount; ++i){
					if (constraint+i == distanceBatch.ConstraintCount || 
						distanceBatch.springIndices[(constraint+i)*2] != lastParticle)
						return constraint+i-1;
					lastParticle = distanceBatch.springIndices[(constraint+i)*2+1];
				}

				return constraint + maxAmount;

			}else{

				int lastParticle = distanceBatch.springIndices[constraint*2];

				for (int i = 1; i <= maxAmount; ++i){
					if (constraint-i < 0 || 
						distanceBatch.springIndices[(constraint-i)*2+1] != lastParticle)
						return constraint-i+1;
					lastParticle = distanceBatch.springIndices[(constraint-i)*2];
				}

				return constraint - maxAmount;

			}
		}

		private int AddParticles(int amount)
		{

			// get constraint batches:
			ObiDistanceConstraintBatch distanceBatch = rope.DistanceConstraints.GetFirstBatch();
			ObiBendConstraintBatch bendingBatch = rope.BendingConstraints.GetFirstBatch();

			amount = Mathf.Min(amount,rope.PooledParticles); 

			// if no particles can be added, just return.
			if (amount == 0)
				return 0;

			// find current constraint and hot constraint:
			int constraint = rope.GetConstraintIndexAtNormalizedCoordinate(normalizedCoord);

			rope.DistanceConstraints.RemoveFromSolver(null);
			rope.BendingConstraints.RemoveFromSolver(null);

			// find indices of first N inactive particles. we'll need them to create new rope.
			// the first and last particle indices in this array will be the ones in the current constraint.
			int[] newParticleIndices = new int[amount+2];
			for (int i = 0, j = 0; i < amount && j < rope.TotalParticles; ++j){
				if (!rope.active[j]){
					newParticleIndices[i+1] = j;
					rope.active[j] = true;
					rope.invMasses[j] = 1.0f/ObiRope.DEFAULT_PARTICLE_MASS;
					++i;
				}
			}

			// TODO: closed curves have a different amount of bend constraints!
			if (direction){

				// fill first and last indices of the new particles array with the ones in the current constraint:
				newParticleIndices[0] = distanceBatch.springIndices[constraint*2];
				newParticleIndices[newParticleIndices.Length-1] = distanceBatch.springIndices[constraint*2+1];

				// update normalized coord:
				normalizedCoord = constraint / (float)(distanceBatch.ConstraintCount + amount);

				Vector4 refPosition1 = rope.Solver.positions[rope.particleIndices[newParticleIndices[0]]];
				Vector4 refPosition2 = rope.Solver.positions[rope.particleIndices[newParticleIndices[newParticleIndices.Length-1]]];

				// update constraints:
				distanceBatch.SetParticleIndex(constraint,newParticleIndices[newParticleIndices.Length-2],ObiDistanceConstraintBatch.DistanceIndexType.First,rope.Closed);
				bendingBatch.SetParticleIndex(constraint,newParticleIndices[newParticleIndices.Length-2],ObiBendConstraintBatch.BendIndexType.First,rope.Closed);
				bendingBatch.SetParticleIndex(constraint-1,newParticleIndices[1],ObiBendConstraintBatch.BendIndexType.Second,rope.Closed);

				// add constraints and particles:
				for (int i = 1; i < newParticleIndices.Length-1; ++i){

					Vector4 pos = refPosition1 + (refPosition2 - refPosition1) * i/(float)(newParticleIndices.Length-1) * 0.5f;

					rope.Solver.positions [rope.particleIndices[newParticleIndices[i]]] = pos;
					rope.Solver.velocities[rope.particleIndices[newParticleIndices[i]]] = Vector4.zero;

					int newConstraintIndex = constraint+i-1;
					distanceBatch.InsertConstraint(newConstraintIndex,newParticleIndices[i-1],newParticleIndices[i],rope.InterparticleDistance,0,0);
					bendingBatch.InsertConstraint(newConstraintIndex,newParticleIndices[i-1],newParticleIndices[i+1],newParticleIndices[i],0,0,0);
				}

			}else{

				// fill first and last indices of the new particles array with the ones in the current constraint:
				newParticleIndices[0] = distanceBatch.springIndices[constraint*2+1];
				newParticleIndices[newParticleIndices.Length-1] = distanceBatch.springIndices[constraint*2];

				// update normalized coord:
				normalizedCoord = (constraint + amount) / (float)(distanceBatch.ConstraintCount + amount);

				Vector4 refPosition1 = rope.Solver.positions[rope.particleIndices[newParticleIndices[0]]];
				Vector4 refPosition2 = rope.Solver.positions[rope.particleIndices[newParticleIndices[newParticleIndices.Length-1]]];

				// update constraints:
				distanceBatch.SetParticleIndex(constraint,newParticleIndices[newParticleIndices.Length-2],ObiDistanceConstraintBatch.DistanceIndexType.Second,rope.Closed);
				bendingBatch.SetParticleIndex(constraint,newParticleIndices[1],ObiBendConstraintBatch.BendIndexType.First,rope.Closed);
				bendingBatch.SetParticleIndex(constraint-1,newParticleIndices[newParticleIndices.Length-2],ObiBendConstraintBatch.BendIndexType.Second,rope.Closed);

				// add constraints and particles:
				for (int i = 1; i < newParticleIndices.Length-1; ++i){

					Vector4 pos = refPosition1 + (refPosition2 - refPosition1) * i/(float)(newParticleIndices.Length-1) * 0.5f;

					rope.Solver.positions [rope.particleIndices[newParticleIndices[i]]] = pos;
					rope.Solver.velocities[rope.particleIndices[newParticleIndices[i]]] = Vector4.zero;

					distanceBatch.InsertConstraint(constraint+1,newParticleIndices[i],newParticleIndices[i-1],rope.InterparticleDistance,0,0);
					bendingBatch.InsertConstraint(constraint,newParticleIndices[i+1],newParticleIndices[i-1],newParticleIndices[i],0,0,0);
				}

			}

			rope.DistanceConstraints.AddToSolver(null);
			rope.BendingConstraints.AddToSolver(null);
			rope.PushDataToSolver(ParticleData.ACTIVE_STATUS);

			rope.UsedParticles += amount;

			rope.RegenerateRestPositions();

			return amount;
		}	


		/**
		 * Removes a certain amount of particles and constraints from the rope, at the point and direction specified:
		 */
		private int RemoveParticles(int amount)
		{
			// get constraint batches:
			ObiDistanceConstraintBatch distanceBatch = rope.DistanceConstraints.GetFirstBatch();
			ObiBendConstraintBatch bendingBatch = rope.BendingConstraints.GetFirstBatch();

			amount = Mathf.Min(amount,rope.UsedParticles-2); 

			// find current constraint and hot constraint:
			int constraint = rope.GetConstraintIndexAtNormalizedCoordinate(normalizedCoord);
			int hotConstraint = FindHotConstraint(distanceBatch, constraint, amount);
			amount = Mathf.Min(amount,Mathf.Abs(hotConstraint-constraint));

			// if no particles can be removed, just return.
			if (amount == 0)
				return 0;

			rope.DistanceConstraints.RemoveFromSolver(null);
			rope.BendingConstraints.RemoveFromSolver(null);

			if (direction){

				// update normalized coord:
				normalizedCoord = constraint / (float)(distanceBatch.ConstraintCount - amount);

				// update constraints:
				distanceBatch.SetParticleIndex(constraint,distanceBatch.springIndices[hotConstraint*2+1],ObiDistanceConstraintBatch.DistanceIndexType.Second,rope.Closed);
				bendingBatch.SetParticleIndex(constraint-1,distanceBatch.springIndices[hotConstraint*2+1],ObiBendConstraintBatch.BendIndexType.Second,rope.Closed);
				bendingBatch.SetParticleIndex(hotConstraint,distanceBatch.springIndices[constraint*2],ObiBendConstraintBatch.BendIndexType.First,rope.Closed);
	
				// remove constraints and particles:
				for (int i = constraint + amount; i > constraint; --i){
					rope.active[distanceBatch.springIndices[i*2]] = false;
					distanceBatch.RemoveConstraint(i);
					bendingBatch.RemoveConstraint(i-1);
				}

			}else{

				// update normalized coord:
				normalizedCoord = (constraint - amount) / (float)(distanceBatch.ConstraintCount - amount);

				// update constraint:
				distanceBatch.SetParticleIndex(constraint,distanceBatch.springIndices[hotConstraint*2],ObiDistanceConstraintBatch.DistanceIndexType.First,rope.Closed);
				bendingBatch.SetParticleIndex(constraint,distanceBatch.springIndices[hotConstraint*2],ObiBendConstraintBatch.BendIndexType.First,rope.Closed);
				bendingBatch.SetParticleIndex(hotConstraint-1,distanceBatch.springIndices[constraint*2+1],ObiBendConstraintBatch.BendIndexType.Second,rope.Closed);
	
				// remove constraints and particles:
				for (int i = constraint-1; i >= constraint - amount; --i){
					rope.active[distanceBatch.springIndices[i*2+1]] = false;
					distanceBatch.RemoveConstraint(i);
					bendingBatch.RemoveConstraint(i);
				}

			}

			rope.DistanceConstraints.AddToSolver(null);
			rope.BendingConstraints.AddToSolver(null);
			rope.PushDataToSolver(ParticleData.ACTIVE_STATUS);

			rope.UsedParticles -= amount;
			
			rope.RegenerateRestPositions();

			return amount;
		}

		/**
		 * Changes the length of the rope, adding or removing particles from its end as needed (as long as there are enough pooled particles
		 * left). Since particles are added/removed to/from the end only, any existing particle data (masses, editor selection data) will
		 * be preserved for existing particles when adding new ones.
		 */
		public void ChangeLength(float newLength){

			if (rope == null)
				return;

			// Clamp new length to sane limits:
			newLength = Mathf.Clamp(newLength,0,(rope.TotalParticles-1) * rope.InterparticleDistance);

			// find current extrusion constraint:
			int constraint = rope.GetConstraintIndexAtNormalizedCoordinate(normalizedCoord);

			// get constraint batch:
			ObiDistanceConstraintBatch distanceBatch = rope.DistanceConstraints.GetFirstBatch();

			// calculate the change in rope length:
			float lengthChange = newLength - rope.RestLength;

			// calculate length change of current constraint:
			float constraintLengthChange = Mathf.Clamp(lengthChange,-distanceBatch.restLengths[constraint],rope.InterparticleDistance - distanceBatch.restLengths[constraint]);
			distanceBatch.restLengths[constraint] += constraintLengthChange;
			lengthChange -= constraintLengthChange;

			// figure out how many particles we need to add (or remove) and what length will remain after that:
			int particleChange = lengthChange > 0 ? Mathf.CeilToInt(lengthChange / rope.InterparticleDistance): Mathf.FloorToInt(lengthChange / rope.InterparticleDistance);
			float remainingLenght = ObiUtils.Mod(lengthChange,rope.InterparticleDistance);

			if (particleChange > 0){

				particleChange = AddParticles(particleChange);
	
				// we cannot add any particles, so extend the segment as much as possible.
				if (particleChange == 0)
					remainingLenght = rope.InterparticleDistance; 

				// Add remaining lenght to the new constraint:
				constraint = rope.GetConstraintIndexAtNormalizedCoordinate(normalizedCoord);
				distanceBatch.restLengths[constraint] = remainingLenght;	

			}else if (particleChange < 0){

				particleChange = RemoveParticles(-particleChange);

				// we cannot remove any particles, so reduce the segment as much as possible:
				if (particleChange == 0)
					remainingLenght = 0;

				// Add remaining lenght to the new constraint:
				constraint = rope.GetConstraintIndexAtNormalizedCoordinate(normalizedCoord);
				distanceBatch.restLengths[constraint] = remainingLenght;	
			}

			distanceBatch.PushDataToSolver(rope.DistanceConstraints);

			// Update rest length:
			rope.RecalculateRestLength();

		}
	}
}

