/////////////////////////////////////////////////////////////////////////
// 
// PicaVoxel - The tiny voxel engine for Unity - http://picavoxel.com
// By Gareth Williams - @garethiw - http://gareth.pw
// 
// Source code distributed under standard Asset Store licence:
// http://unity3d.com/legal/as_terms
//
/////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace PicaVoxel
{
    public static class QubicleImporter
    {
        private static void FromQubicle(BinaryReader stream, QubicleImport root, float voxelSize)
        {
            //const int MAX_VOLUME_DIMENSION = 64;

            uint sizex=0;
            uint sizey=0;
            uint sizez=0;

//            uint version;
            uint colorFormat;
            uint zAxisOrientation;
            uint compressed;
//            uint visibilityMaskEncoded;
            uint numMatrices;

            uint i;
            uint j;
            uint x;
            uint y;
            uint z;

            int posX;
            int posY;
            int posZ;
            uint[,,] matrix;
            List<uint[,,]> matrixList = new List<uint[,,]>();
            uint index;
            uint data;
            uint count;
            const uint CODEFLAG = 2;
            const uint NEXTSLICEFLAG = 6;
 
           //version = stream.ReadUInt32();
            stream.ReadUInt32();
           colorFormat = stream.ReadUInt32();
           zAxisOrientation = stream.ReadUInt32();
           compressed = stream.ReadUInt32();
           //visibilityMaskEncoded = stream.ReadUInt32();
            stream.ReadUInt32();
           numMatrices = stream.ReadUInt32();

            string volumeName = root.name;
            string path = Helper.GetMeshStorePath();
            for (int d = root.transform.childCount - 1; d >= 0; d--)
            {
                Volume vol = root.transform.GetChild(d).GetComponent<Volume>();
                if (vol != null)
                {
                    if (Directory.Exists(path))
                    {
                        DirectoryInfo di = new DirectoryInfo(path);
                        DirectoryInfo[] dirs = di.GetDirectories();
                        for (int dir=0;dir<dirs.Length;dir++)
                        {
                            if (dirs[dir].Name.ToLower() == vol.AssetGuid.ToLower())
                            {
                                dirs[dir].Delete(true);
                                break;
                            }
                        }
                    }
                    GameObject.DestroyImmediate(root.transform.GetChild(d).gameObject);
                }
            }

            for (i = 0; i < numMatrices; i++) // for each matrix stored in file
            {
                 // read matrix name
                 byte nameLength = stream.ReadByte();
                 string name = new string(stream.ReadChars(nameLength));

                 // read matrix size 
                 sizex = stream.ReadUInt32();
                 sizey = stream.ReadUInt32();
                 sizez = stream.ReadUInt32();
   
                 // read matrix position (in this example the position is irrelevant)
                 posX = stream.ReadInt32();
                 posY = stream.ReadInt32();
                 posZ = stream.ReadInt32();
   
                 // create matrix and add to matrix list
                 matrix = new uint[sizex,sizey,sizez];
                 matrixList.Add(matrix);
   
                 if (compressed == 0) // if uncompressd
                 {
                   for(z = 0; z < sizez; z++)
                     for(y = 0; y < sizey; y++)
                         for (x = 0; x < sizex; x++)
                             matrix[x, y, z] = stream.ReadUInt32();
                 }
                 else // if compressed
                 { 
                   z = 0;

                   while (z < sizez) 
                   {
                
                     index = 0;
       
                     while (true) 
                     {
                       data = stream.ReadUInt32();
         
                       if (data == NEXTSLICEFLAG)
                         break;
                       else if (data == CODEFLAG) 
                       {
                         count = stream.ReadUInt32();
                         data = stream.ReadUInt32();
           
                         for(j = 0; j < count; j++) 
                         {
                           x = index % sizex ; // mod = modulo e.g. 12 mod 8 = 4
                           y = index / sizex ; // div = integer division e.g. 12 div 8 = 1
                           index++;
                           matrix[x ,y ,z] = data;
                         }
                       }
                       else 
                       {
                         x = index % sizex;
                         y = index / sizex ;
                         index++;
                         matrix[x,y ,z] = data;
                       }
                     }
                     z++;
                   }
                 }


                var newObject =
                    Editor.Instantiate(EditorUtility.VoxelVolumePrefab, Vector3.zero, Quaternion.identity) as
                        GameObject;
                if (newObject != null)
                {
                    newObject.name = name!=""?name:volumeName;
                    newObject.GetComponent<Volume>().Material = EditorUtility.PicaVoxelDiffuseMaterial;
                    newObject.GetComponent<Volume>().GenerateBasic(FillMode.None);
                    Volume voxelVolume = newObject.GetComponent<Volume>();

                    voxelVolume.XSize = Convert.ToInt32(sizex);
                    voxelVolume.YSize = Convert.ToInt32(sizey);
                    voxelVolume.ZSize = Convert.ToInt32(sizez);
                    voxelVolume.Frames[0].XSize = Convert.ToInt32(sizex);
                    voxelVolume.Frames[0].YSize = Convert.ToInt32(sizey);
                    voxelVolume.Frames[0].ZSize = Convert.ToInt32(sizez);
                    voxelVolume.Frames[0].Voxels = new Voxel[sizex * sizey * sizez];
                    for (int v = 0; v < voxelVolume.Frames[0].Voxels.Length; v++) voxelVolume.Frames[0].Voxels[v].Value = 128;
                    voxelVolume.VoxelSize = voxelSize;

                    if (zAxisOrientation == 0)
                    {
                        voxelVolume.Pivot = new Vector3(sizex, 0, 0)*voxelSize;
                        voxelVolume.UpdatePivot();
                    }

                    for (z = 0; z < sizez; z++)
                        for (y = 0; y < sizey; y++)
                            for (x = 0; x < sizex; x++)
                            {

                                Color col = UIntToColor(matrix[x, y, z], colorFormat);

                                if (matrix[x, y, z] != 0)
                                    voxelVolume.Frames[0].Voxels[(zAxisOrientation == 0 ? sizex - 1 - x : x) + sizex * (y + sizey * z)] = new Voxel()
                                    {
                                        State = VoxelState.Active,
                                        Color = col,
                                        Value = 128
                                    };
                            }


                    voxelVolume.CreateChunks();
                    voxelVolume.SaveForSerialize();

                    newObject.transform.position = (new Vector3((zAxisOrientation == 0 ? -posX : posX), posY, posZ) * voxelSize);
                    newObject.transform.parent = root.transform;
                }
           // }
            }


            
        }

        public static void QubicleImport(string fn, string volumeName, float voxelSize)
        {
            var newObject = new GameObject();

            newObject.name = (volumeName != "Qubicle Import" ? volumeName : Path.GetFileNameWithoutExtension(fn));
            newObject.AddComponent<QubicleImport>();
            newObject.GetComponent<QubicleImport>().ImportedFile = fn;
            newObject.GetComponent<QubicleImport>().ImportedVoxelSize = voxelSize;

            using (BinaryReader stream = new BinaryReader(new FileStream(fn, FileMode.Open)))
            {
                FromQubicle(stream, newObject.GetComponent<QubicleImport>(), voxelSize);
            }
        }
        public static void QubicleImport(QubicleImport existingImport)
        {
            using (BinaryReader stream = new BinaryReader(new FileStream(existingImport.ImportedFile, FileMode.Open)))
            {
                FromQubicle(stream, existingImport, existingImport.ImportedVoxelSize);
            }
        }

        private static Color32 UIntToColor(uint color, uint format)
        {
            byte r = 0;
            byte g = 0;
            byte b = 0;

            if (format == 0)
            {
                r = (byte)(color >> 0);
                g = (byte)(color >> 8);
                b = (byte)(color >> 16);
            }
            else
            {
                r = (byte)(color >> 16);
                g = (byte)(color >> 8);
                b = (byte)(color >> 0);
            }

            return new Color32(r, g, b, 255);
        }

    }
}
