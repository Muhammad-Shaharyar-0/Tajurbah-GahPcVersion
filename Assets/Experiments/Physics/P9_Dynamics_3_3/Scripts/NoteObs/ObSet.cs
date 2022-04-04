using System.Collections;
using System.Collections.Generic;

namespace P9_Dynamics_3_3
{
    public class ObSet
    {
        public float s, m1, m2, t;
        public bool actual;
        // Start is called before the first frame update
        public ObSet()
        {
            s = m1 = m2 = t = -1.0f;
            actual = false;
        }

        public string inspect()
        {
            return "s=" + s.ToString() + ",m1=" + m1.ToString() + ",m2=" + m2.ToString() + ",t=" + t.ToString() +
                    ",actual=" + actual;
        }

    }
}
