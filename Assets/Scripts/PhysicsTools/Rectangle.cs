using UnityEngine;
namespace PhysicsTools
{
    /// <summary>
    /// class that stores information about rects
    /// </summary>
    public class Rectangle  {
        /// <summary>
        /// rect's collider
        /// </summary>
        BoxCollider2D box;
        /// <summary>
        /// list of the rect's corners coordinates
        /// </summary>
        Vector2[] Corners;

        // /// <summary>
        // /// center of the rect
        // /// </summary>
        // Vector2 center;

        /// <summary>
        /// point
        /// </summary>
        private Vector2 a, b, c, d;


        public Vector2 A
        {
            get
            {
                return a;
            }

            set
            {
                a = value;
            }
        }

        public Vector2 B
        {
            get
            {
                return b;
            }

            set
            {
                b = value;
            }
        }

        public Vector2 C
        {
            get
            {
                return c;
            }

            set
            {
                c = value;
            }
        }

        public Vector2 D
        {
            get
            {
                return d;
            }

            set
            {
                d = value;
            }
        }

        /// <summary>
        /// constructor of the rect from a collider
        /// </summary>
        /// <param name="collider">box collider the rect is caluclated from</param>
        /// <param name="lambda">scale of the rect relatively  box</param>
        public Rectangle(BoxCollider2D collider, float lambda)
        {
            box = collider;

            // center = box.bounds.center;

            A = (box.transform.TransformPoint(new Vector2(-box.size.x / 2, box.size.y / 2)) - box.bounds.center) *(1- lambda) + box.bounds.center;

            B = (box.transform.TransformPoint(new Vector2(box.size.x / 2, box.size.y / 2)) - box.bounds.center) *(1 - lambda) + box.bounds.center;

            C = (box.transform.TransformPoint(new Vector2(box.size.x / 2, -box.size.y / 2)) - box.bounds.center) *(1 - lambda) + box.bounds.center;

            D = (box.transform.TransformPoint(new Vector2(-box.size.x / 2, -box.size.y / 2)) - box.bounds.center) *(1 - lambda) + box.bounds.center;

            
            Corners = new Vector2[4];
            Corners[0] = A;
            Corners[1] = B;
            Corners[2] = C;
            Corners[3] = D;
        }
        /// <summary>
        /// setter to reuse the same object
        /// </summary>
        /// <param name="collider">collider the rect is calculated from</param>
        /// <param name="lambda">scale of the rect relatively to the box</param>
        public void Reset(BoxCollider2D collider, float lambda)
        {
            box = collider;

            // center = box.bounds.center;

            A = (box.transform.TransformPoint(new Vector2(-box.size.x / 2, box.size.y / 2)) - box.bounds.center) * (1 - lambda) + box.bounds.center;

            B = (box.transform.TransformPoint(new Vector2(box.size.x / 2, box.size.y / 2)) - box.bounds.center) * (1 - lambda) + box.bounds.center;

            C = (box.transform.TransformPoint(new Vector2(box.size.x / 2, -box.size.y / 2)) - box.bounds.center) * (1 - lambda) + box.bounds.center;

            D = (box.transform.TransformPoint(new Vector2(-box.size.x / 2, -box.size.y / 2)) - box.bounds.center) * (1 - lambda) + box.bounds.center;


            Corners = new Vector2[4];
            Corners[0] = A;
            Corners[1] = B;
            Corners[2] = C;
            Corners[3] = D;
        }

    }
}
