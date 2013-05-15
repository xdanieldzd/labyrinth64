using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Labyrinth.ModelImport
{
    class WavefrontObj : I3DModel
    {
        static char[] TokenSeperator = { ' ', '\t' };
        static char[] TokenValSeperator = { '/' };
        static List<string> ValidImageTypes = new List<string>(new string[] { ".bmp", ".gif", ".jpg", ".jpeg", ".png", ".tiff", ".tif" });

        public Common.Model Model { get; set; }

        bool groupopen, matopen;
        string mtlpath, mapka, mapkd;
        Common.Color4 ka, kd;
        double tr;

        List<Common.Vector3> vertpos, normals;
        List<Common.Vector2> texcoords;
        List<Common.Color4> colors;

        public WavefrontObj()
        {
            Model = new Common.Model();

            vertpos = new List<Common.Vector3>();
            normals = new List<Common.Vector3>();
            texcoords = new List<Common.Vector2>();
            colors = new List<Common.Color4>();
        }

        public WavefrontObj(string fn)
            : this()
        {
            Open(fn);
        }

        public void Open(string fn)
        {
            fn = Path.GetFullPath(fn);

            StreamReader sr = File.OpenText(fn);

            Common.Group group = new Common.Group();
            groupopen = false;

            string line = string.Empty;
            string curmatname = string.Empty;

            while ((line = sr.ReadLine()) != null)
            {
                line = line.TrimStart(TokenSeperator);
                if (line == string.Empty) continue;

                string[] tokenized = line.Split(TokenSeperator, StringSplitOptions.RemoveEmptyEntries);

                switch (tokenized[0])
                {
                    case "#":
                        /* Comment */
                        break;

                    case "o":
                        /* Object */
                        Model.Name = line.Substring(line.IndexOf(tokenized[1]));
                        break;

                    case "g":
                        /* Group */
                        if (groupopen) TryAddGroup(group);

                        groupopen = true;
                        group = new Common.Group();
                        group.Name = line.Substring(line.IndexOf(tokenized[1]));
                        break;

                    case "mtllib":
                        /* Material lib reference */
                        OpenMtl((mtlpath = CreateFullPath(fn, line.Substring(line.IndexOf(tokenized[1])))));
                        break;

                    case "v":
                        /* Vertex */
                        vertpos.Add(new Common.Vector4(
                            double.Parse(tokenized[1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture),
                            double.Parse(tokenized[2], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture),
                            double.Parse(tokenized[3], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture),
                            (tokenized.Length == 5 ? double.Parse(tokenized[4], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture) : 0.0)));
                        break;

                    case "vt":
                        /* Texture coordinates */
                        texcoords.Add(new Common.Vector3(
                            double.Parse(tokenized[1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture),
                            -double.Parse(tokenized[2], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture),
                            (tokenized.Length == 4 ? double.Parse(tokenized[3], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture) : 0.0)));
                        break;

                    case "vn":
                        /* Normals */
                        normals.Add(new Common.Vector3(
                            double.Parse(tokenized[1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture),
                            double.Parse(tokenized[2], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture),
                            double.Parse(tokenized[3], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture)));
                        break;

                    case "vc":
                        /* Colors */
                        colors.Add(new Common.Color4(
                            double.Parse(tokenized[1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture),
                            double.Parse(tokenized[2], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture),
                            double.Parse(tokenized[3], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture),
                            (tokenized.Length == 5 ? double.Parse(tokenized[4], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture) : 0.0)));
                        break;

                    case "usemtl":
                        /* Material to use */
                        curmatname = line.Substring(line.IndexOf(tokenized[1]));
                        break;

                    case "f":
                        /* Faces - http://z64.spinout182.com/index.php?topic=320.msg2229#msg2229 */
                        int[] VIndex = new int[tokenized.Length - 1];
                        int[] TIndex = new int[tokenized.Length - 1];
                        int[] NIndex = new int[tokenized.Length - 1];
                        int[] CIndex = new int[tokenized.Length - 1];

                        /* Triangulate face */
                        for (int i = 0; i < tokenized.Length - 1; i++)
                        {
                            string[] TokenizedVals = tokenized[i + 1].Split(TokenValSeperator);
                            int[] VLocal = new int[3];
                            int[] TLocal = new int[3];
                            int[] NLocal = new int[3];
                            int[] CLocal = new int[3];

                            int.TryParse(TokenizedVals[0], out VIndex[i]);
                            int.TryParse(TokenizedVals[1], out TIndex[i]);
                            if (TokenizedVals.Length >= 3)
                                int.TryParse(TokenizedVals[2], out NIndex[i]);
                            if (TokenizedVals.Length >= 4)
                                int.TryParse(TokenizedVals[3], out CIndex[i]);

                            VIndex[i] -= 1;
                            TIndex[i] -= 1;
                            NIndex[i] -= 1;
                            CIndex[i] -= 1;

                            /* Last vertex of triangle, or index to next point in the face (e.g. quad) */
                            if (i >= 2)
                            {
                                if (VIndex[0 + (i - 2)] != -1 && VIndex[1 + (i - 2)] != -1 && VIndex[2 + (i - 2)] != -1)
                                {
                                    VLocal[0] = VIndex[0]; TLocal[0] = TIndex[0]; NLocal[0] = NIndex[0]; CLocal[0] = CIndex[0];
                                    VLocal[1] = VIndex[i - 1]; TLocal[1] = TIndex[i - 1]; NLocal[1] = NIndex[i - 1]; CLocal[1] = CIndex[i - 1];
                                    VLocal[2] = VIndex[i]; TLocal[2] = TIndex[i]; NLocal[2] = NIndex[i]; CLocal[2] = CIndex[i];

                                    Common.Vertex[] curverts = new Common.Vertex[3];

                                    for (int j = 0; j < curverts.Length; j++)
                                    {
                                        Common.Vector3 pos = (VIndex[j] == -1 ? Common.Vector3.Zero : vertpos[VIndex[j]]);
                                        Common.Vector2 tc = (TIndex[j] == -1 ? Common.Vector2.Zero : texcoords[TIndex[j]]);
                                        Common.Color4 vc = (CIndex[j] == -1 ? new Common.Color4(0.2, 0.2, 0.2, 1.0) : colors[CIndex[j]]);
                                        Common.Vector3 norm = (NIndex[j] == -1 ? Common.Vector3.Zero : normals[NIndex[j]]);

                                        //if (j == 1) vc = new Common.Color4(1.0, 0.2, 0.2, 0.2); //force something for testing

                                        curverts[j] = new Common.Vertex(pos, tc, vc, norm);
                                    }

                                    group.Polygons.Add(new Common.Polygon(curverts[0], curverts[1], curverts[2], new Common.Material() { Name = curmatname }));
                                }
                            }
                        }

                        break;
                }
            }

            TryAddGroup(group);

            foreach (Common.Group g in Model.Groups)
            {
                foreach (Common.Polygon p in g.Polygons)
                {
                    p.Material = Model.Materials.FirstOrDefault(x => x.Name == p.Material.Name);
                }
            }

            sr.Close();
        }

        private void OpenMtl(string fn)
        {
            fn = Path.GetFullPath(fn);

            StreamReader sr = File.OpenText(fn);

            Common.Material mat = new Common.Material();
            matopen = false;
            mapka = mapkd = string.Empty;
            ka = kd = Common.Color4.Zero;
            tr = 0.0;

            string line = string.Empty;

            while ((line = sr.ReadLine()) != null)
            {
                line = line.TrimStart(TokenSeperator);
                if (line == string.Empty) continue;

                string[] tokenized = line.Split(TokenSeperator, StringSplitOptions.RemoveEmptyEntries);

                switch (tokenized[0])
                {
                    case "#":
                        /* Comment */
                        break;

                    case "newmtl":
                        /* New material */
                        if (matopen) TryAddMaterial(mat);

                        matopen = true;
                        mat = new Common.Material();
                        mat.Name = line.Substring(line.IndexOf(tokenized[1]));
                        mapka = mapkd = string.Empty;
                        ka = kd = Common.Color4.Zero;
                        tr = 0.0;
                        break;

                    case "Ka":
                        /* Ambient color */
                        ka = new Common.Color4(
                            double.Parse(tokenized[1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture),
                            double.Parse(tokenized[2], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture),
                            double.Parse(tokenized[3], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture),
                            1.0);
                        break;

                    case "Kd":
                        /* Diffuse color */
                        kd = new Common.Color4(
                            double.Parse(tokenized[1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture),
                            double.Parse(tokenized[2], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture),
                            double.Parse(tokenized[3], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture),
                            1.0);
                        break;

                    case "Tr":
                    case "d":
                        /* Transparency */
                        double nval = double.Parse(tokenized[1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
                        if (tr == 0.0) tr = nval;
                        break;

                    case "map_Ka":
                    case "mapKa":
                        /* Ambient texture map */
                        mapka = CreateFullPath(fn, line.Substring(line.IndexOf(tokenized[1])));
                        break;

                    case "map_Kd":
                    case "mapKd":
                        /* Diffuse texture map */
                        mapkd = CreateFullPath(fn, line.Substring(line.IndexOf(tokenized[1])));
                        break;
                }
            }

            TryAddMaterial(mat);

            sr.Close();
        }

        private string CreateFullPath(string p1, string p2)
        {
            string path = p2;
            if (!Path.IsPathRooted(path)) path = Path.Combine(Path.GetDirectoryName(p1), path);
            return Path.GetFullPath(path);
        }

        private void TryAddGroup(Common.Group group)
        {
            if (groupopen || group.Name == null)
            {
                Model.Groups.Add(group);
                groupopen = false;
            }
        }

        private void TryAddMaterial(Common.Material mat)
        {
            if (matopen)
            {
                if (mapka != string.Empty)
                {
                    /* Ambient texture map is set, use ambient texture map & color */
                    mat.TextureMap = mapka;
                    mat.Color = ka;
                }
                else if (mapkd != string.Empty)
                {
                    /* Diffuse texture map is set, use diffuse texture map & color */
                    mat.TextureMap = mapkd;
                    mat.Color = kd;
                }
                else if (ka != Common.Color4.Zero)
                {
                    /* Ambient color is set but no texture map, use ambient color */
                    mat.Color = ka;
                }
                else if (kd != Common.Color4.Zero)
                {
                    /* Diffuse color is set but no texture map, use diffuse color */
                    mat.Color = kd;
                }

                /* Transparency is set to something other than 0, use transparency */
                if (tr != 0.0) mat.Color.A = tr;

                if (mat.TextureMap != string.Empty)
                {
                    /* Load texture map to Bitmap */
                    mat.TextureMapImage = new System.Drawing.Bitmap(mat.TextureMap);
                }

                Model.Materials.Add(mat);
                matopen = false;
            }
        }
    }
}
