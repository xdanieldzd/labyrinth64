using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Labyrinth.ModelImport
{
    class Common
    {
        public class Vector2
        {
            public static Vector2 Zero = new Vector2(0.0, 0.0);

            public double X { get; set; }//end method
            public double Y { get; set; }//end method

            public Vector2()
            {
                this.X = Zero.X;
                this.Y = Zero.Y;
            }//end constructor

            public Vector2(double x, double y)
            {
                this.X = x;
                this.Y = y;
            }//end constructor

            public override int GetHashCode()
            {
                int hash = 17;
                hash *= 23 + this.X.GetHashCode();
                hash *= 23 + this.Y.GetHashCode();
                return hash;
            }//end method

            public override bool Equals(object obj)
            {
                if (obj == null) 
                    return false;

                Vector2 vec = obj as Vector2;
                if ((object)vec == null) 
                    return false;

                return ((this.X == vec.X) && (this.Y == vec.Y));
            }//end method

            public static bool operator ==(Vector2 a, Vector2 b)
            {
                if (object.ReferenceEquals(a, b)) 
                    return true;

                if (((object)a == null) || ((object)b == null)) 
                    return false;

                return ((a.X == b.X) && (a.Y == b.Y));
            }//end method

            public static bool operator !=(Vector2 a, Vector2 b)
            {
                return !(a == b);
            }//end method

            public override string ToString()
            {
                return string.Format("({0}, {1})", this.X, this.Y);
            }//end method
        }//end vector2 class

        public class Vector3 : Vector2
        {
            public static new Vector3 Zero = new Vector3(0.0, 0.0, 0.0);

            public double Z { get; set; }//end method

            public Vector3()
            {
                this.Z = Zero.Z;
            }//end constructor

            public Vector3(double x, double y, double z)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
            }//end constructor

            public override int GetHashCode()
            {
                int hash = 17;
                hash *= 23 + this.X.GetHashCode();
                hash *= 23 + this.Y.GetHashCode();
                hash *= 23 + this.Z.GetHashCode();
                return hash;
            }//end method

            public override bool Equals(object obj)
            {
                if (obj == null) 
                    return false;

                Vector3 vec = obj as Vector3;
                if ((object)vec == null) 
                    return false;

                return ((this.X == vec.X) && (this.Y == vec.Y) && (this.Z == vec.Z));
            }//end method

            public static bool operator ==(Vector3 a, Vector3 b)
            {
                if (object.ReferenceEquals(a, b)) 
                    return true;

                if (((object)a == null) || ((object)b == null)) 
                    return false;

                return ((a.X == b.X) && (a.Y == b.Y) && (a.Z == b.Z));
            }//end method

            public static bool operator !=(Vector3 a, Vector3 b)
            {
                return !(a == b);
            }//end method

            public override string ToString()
            {
                return string.Format("({0}, {1}, {2})", this.X, this.Y, this.Z);
            }//end method
        }//end Vector3 class

        public class Vector4 : Vector3
        {
            public static new Vector4 Zero = new Vector4(0.0, 0.0, 0.0, 0.0);

            public double W { get; set; }//end method

            public Vector4()
            {
                this.W = Zero.W;
            }//end constructor

            public Vector4(double x, double y, double z, double w)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
                this.W = w;
            }//end constructor

            public override int GetHashCode()
            {
                int hash = 17;
                hash *= 23 + this.X.GetHashCode();
                hash *= 23 + this.Y.GetHashCode();
                hash *= 23 + this.Z.GetHashCode();
                hash *= 23 + this.W.GetHashCode();
                return hash;
            }//end method

            public override bool Equals(object obj)
            {
                if (obj == null) 
                    return false;

                Vector4 vec = obj as Vector4;
                if ((object)vec == null) 
                    return false;

                return ((this.X == vec.X) && (this.Y == vec.Y) && (this.Z == vec.Z) && (this.W == vec.W));
            }//end method

            public static bool operator ==(Vector4 a, Vector4 b)
            {
                if (object.ReferenceEquals(a, b)) 
                    return true;

                if (((object)a == null) || ((object)b == null)) 
                    return false;

                return ((a.X == b.X) && (a.Y == b.Y) && (a.Z == b.Z) && (a.W == b.W));
            }//end method

            public static bool operator !=(Vector4 a, Vector4 b)
            {
                return !(a == b);
            }//end method

            public override string ToString()
            {
                return string.Format("({0}, {1}, {2}, {3})", this.X, this.Y, this.Z, this.W);
            }//end method
        }//end Vector4 class

        public class Color4 : Vector4
        {
            public static new Color4 Zero = new Color4(0.0, 0.0, 0.0, 0.0);
            public static Color4 White = new Color4(1.0, 1.0, 1.0, 1.0);

            public double R { get { return X; } set { X = value; } }//end method
            public double G { get { return Y; } set { Y = value; } }//end method
            public double B { get { return Z; } set { Z = value; } }//end method
            public double A { get { return W; } set { W = value; } }//end method

            public Color4()
                : base()
            {
                this.R = White.R;
                this.G = White.G;
                this.B = White.B;
                this.A = White.A;
            }//end constructor

            public Color4(double r, double g, double b, double a) : base(r, g, b, a) { }//end constructor
        }//end Color4 class

        public class Vertex
        {
            public Vector3 Position { get; set; }//end method
            public Vector2 TextureCoordinates { get; set; }//end method
            public Color4 Color { get; set; }//end method
            public Vector3 Normals { get; set; }//end method

            public Vertex()
            {
                Position = new Vector3();
                TextureCoordinates = new Vector2();
                Color = new Color4();
                Normals = new Vector3();
            }//end constructor

            public Vertex(Vector3 position, Vector2 texcoords, Color4 color, Vector3 normals)
            {
                Position = position;
                TextureCoordinates = texcoords;
                Color = color;
                Normals = normals;
            }//end constructor
        }//end Vertex class

        public class Material
        {
            public string Name { get; set; }//end method
            public string TextureMap { get; set; }//end method
            public Color4 Color { get; set; }//end method
            public Bitmap TextureMapImage { get; set; }//end method

            public Material()
            {
                Name = string.Empty;
                TextureMap = string.Empty;
                Color = new Color4();
            }//end constructor
        }//end Material class

        public class Polygon
        {
            public List<Vertex> Vertices { get; set; }//end method
            public Material Material { get; set; }//end method

            public Polygon()
            {
                Vertices = new List<Vertex>();
                Material = null;
            }//end constructor

            public Polygon(Vertex v1, Vertex v2, Vertex v3)
                : this()
            {
                Vertices.Add(v1);
                Vertices.Add(v2);
                Vertices.Add(v3);
            }//end constructor

            public Polygon(Vertex v1, Vertex v2, Vertex v3, Material mat)
                : this(v1, v2, v3)
            {
                Material = mat;
            }//end constructor

            public Polygon(Vertex v1, Vertex v2, Vertex v3, Vertex v4)
                : this()
            {
                Vertices.Add(v1);
                Vertices.Add(v2);
                Vertices.Add(v3);
                Vertices.Add(v4);
            }//end constructor

            public Polygon(Vertex v1, Vertex v2, Vertex v3, Vertex v4, Material mat)
                : this(v1, v2, v3, v4)
            {
                Material = mat;
            }//end constructor
        }//end Polygon class

        public class Group
        {
            public string Name { get; set; }//end method

            public List<Polygon> Polygons { get; set; }//end method

            public Group()
            {
                Name = string.Empty;
                Polygons = new List<Polygon>();
            }//end constructor
        }//end Group class

        public class Model
        {
            public string Name { get; set; }//end method
            public List<Common.Group> Groups { get; set; }//end method
            public List<Common.Material> Materials { get; set; }//end method

            public Model()
            {
                Name = string.Empty;
                Groups = new List<Group>();
                Materials = new List<Material>();
            }//end constructor

            public Model(string name, List<Group> groups, List<Material> mats)
            {
                Name = name;
                Groups = groups;
                Materials = mats;
            }//end constructor
        }//end Model class
    }//end Common class
}//end namespace
