using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PicaVoxel;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace PicaVoxel
{
    public partial class VolumeEditor
    {
        static bool scrollFoldout = false;
        static bool chunkFoldout = true;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUIStyle foldoutStyle = EditorStyles.foldout;
            foldoutStyle.fontStyle = FontStyle.Bold;

            EditorGUILayout.Space();
            EditorUtility.SkinnedLabel("Editor");

            if (!runtimeOnlyMesh.boolValue)
            {
                if (serializedObject.targetObjects.Length < 2)
                {
                    if (GUILayout.Button(voxelObject.IsEnabledForEditing ? "Stop Editing" : "Start Editing"))
                    {
                        if (voxelObject.IsEnabledForEditing)
                            voxelObject.SaveForSerialize();

                        voxelObject.IsEnabledForEditing = !voxelObject.IsEnabledForEditing;
                        voxelObject.PaintMode = EditorPaintMode.Color;

                        voxelObject.UpdateChunks(true);



                        SceneView.RepaintAll();
                    }
                    if (voxelObject.IsEnabledForEditing)
                    {
                        propogateAllFrames = EditorGUILayout.ToggleLeft(" Propagate edits to all frames",
                            propogateAllFrames);

                        EditorGUILayout.BeginHorizontal();
                        drawGrid = EditorGUILayout.ToggleLeft(new GUIContent(" Draw Grid"), drawGrid);
                        if (drawGrid != voxelObject.DrawGrid)
                        {
                            voxelObject.DrawGrid = drawGrid;
                            SceneView.RepaintAll();
                        }
                        drawMesh = EditorGUILayout.ToggleLeft(new GUIContent(" Draw Wireframe"), drawMesh);
                        if (drawMesh != voxelObject.DrawMesh)
                        {
                            voxelObject.DrawMesh = drawMesh;
                            SceneView.RepaintAll();
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }

            runtimeOnlyMesh.boolValue = EditorGUILayout.ToggleLeft(new GUIContent(" Runtime-Only Mesh"),
                runtimeOnlyMesh.boolValue);
            if (runtimeOnlyMesh.boolValue != voxelObject.RuntimOnlyMesh)
            {
                foreach (var o in serializedObject.targetObjects)
                {
                    ((Volume) o).IsEnabledForEditing = false;
                    ((Volume) o).RuntimOnlyMesh = runtimeOnlyMesh.boolValue;
                    ((Volume) o).CreateChunks();
                    ((Volume) o).UpdateAllChunks();
                }
            }

            EditorGUILayout.Space();

            EditorUtility.SkinnedLabel(
                "Size (Volume size: " + voxelObject.XSize + "," + voxelObject.YSize + "," + voxelObject.ZSize +
                " Chunk Size: " + voxelObject.XChunkSize + "," + voxelObject.YChunkSize + "," + voxelObject.ZChunkSize +
                ")");
            float size = EditorGUILayout.FloatField("Voxel Size:", voxelSizeProperty.floatValue);
            if (size != voxelSizeProperty.floatValue && size > 0f)
            {
                voxelSizeProperty.floatValue = size;
                voxelSize = voxelSizeProperty.floatValue;
                foreach (var o in serializedObject.targetObjects)
                {
                    ((Volume) o).VoxelSize = voxelSize;
                    ((Volume) o).CreateChunks();
                }
            }

            float overlap = EditorGUILayout.FloatField("Face Overlap:", overlapAmountProperty.floatValue);
            if (overlap != overlapAmountProperty.floatValue)
            {
                overlapAmountProperty.floatValue = overlap;
                overlapAmount = overlapAmountProperty.floatValue;
                foreach (var o in serializedObject.targetObjects)
                {
                    ((Volume) o).OverlapAmount = overlapAmount;
                    ((Volume) o).CreateChunks();
                }
            }

            if (serializedObject.targetObjects.Length < 2 && !voxelObject.IsEnabledForEditing)
            {
                if (GUILayout.Button("Resize"))
                {
                    EditorResizeWindow window =
                        (EditorResizeWindow)
                            EditorWindow.GetWindowWithRect((typeof (EditorResizeWindow)), new Rect(100, 100, 400, 240),
                                true);
                    window.Init(voxelObject);
                }

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Rotate X"))
                {
                    voxelObject.RotateX();
                }
                if (GUILayout.Button("Rotate Y"))
                {
                    voxelObject.RotateY();
                }
                if (GUILayout.Button("Rotate Z"))
                {
                    voxelObject.RotateZ();
                }
                GUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();
            EditorUtility.SkinnedLabel("Pivot");
            pivotProperty.vector3Value = EditorGUILayout.Vector3Field("", pivotProperty.vector3Value, null);
            if (pivotProperty.vector3Value != voxelObject.Pivot)
            {
                pivot = pivotProperty.vector3Value;
                foreach (var o in serializedObject.targetObjects)
                {
                    ((Volume) o).Pivot = pivot;
                    ((Volume) o).UpdatePivot();
                }
            }
            if (GUILayout.Button("Set to Center"))
            {
                pivot = (new Vector3(voxelObject.XSize, voxelObject.YSize, voxelObject.ZSize)*voxelObject.VoxelSize)/2f;
                pivotProperty.vector3Value = pivot;
                foreach (var o in serializedObject.targetObjects)
                {
                    ((Volume) o).Pivot = pivot;
                    ((Volume) o).UpdatePivot();
                }
            }

            EditorGUILayout.Space();
            scrollFoldout = EditorGUILayout.Foldout(scrollFoldout, "Scroll Voxels", foldoutStyle);
            if (scrollFoldout)
            {
                //EditorUtility.SkinnedLabel("Scroll Voxels");
                allFrames = EditorGUILayout.ToggleLeft(" Scroll all frames", allFrames);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("X-1"))
                {
                    voxelObject.ScrollX(-1, allFrames);
                }
                if (GUILayout.Button("X-10"))
                {
                    voxelObject.ScrollX(-10, allFrames);
                }
                if (GUILayout.Button("X+1"))
                {
                    voxelObject.ScrollX(1, allFrames);
                }
                if (GUILayout.Button("X+10"))
                {
                    voxelObject.ScrollX(10, allFrames);
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Y-1"))
                {
                    voxelObject.ScrollY(-1, allFrames);
                }
                if (GUILayout.Button("Y-10"))
                {
                    voxelObject.ScrollY(-10, allFrames);
                }
                if (GUILayout.Button("Y+1"))
                {
                    voxelObject.ScrollY(1, allFrames);
                }
                if (GUILayout.Button("Y+10"))
                {
                    voxelObject.ScrollY(10, allFrames);
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Z-1"))
                {
                    voxelObject.ScrollZ(-1, allFrames);
                }
                if (GUILayout.Button("Z-10"))
                {
                    voxelObject.ScrollZ(-10, allFrames);
                }
                if (GUILayout.Button("Z+1"))
                {
                    voxelObject.ScrollZ(1, allFrames);
                }
                if (GUILayout.Button("Z+10"))
                {
                    voxelObject.ScrollZ(10, allFrames);
                }
                GUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();
            chunkFoldout = EditorGUILayout.Foldout(chunkFoldout, "Chunk Generation", foldoutStyle);
            if (chunkFoldout)
            {

                chunkLayer.intValue = EditorGUILayout.LayerField("Chunk Layer: ", chunkLayer.intValue);
                if (chunkLayer.intValue != voxelObject.ChunkLayer)
                {
                    foreach (var o in serializedObject.targetObjects)
                    {
                        ((Volume) o).ChunkLayer = chunkLayer.intValue;
                        ((Volume) o).CreateChunks();
                    }
                }

                EditorUtility.SkinnedLabel("Mesh Collider");
                collisionMode.enumValueIndex =
                    Convert.ToInt16(EditorGUILayout.EnumPopup((CollisionMode) collisionMode.enumValueIndex));
                if (collisionMode.enumValueIndex != Convert.ToInt16(voxelObject.CollisionMode))
                {
                    foreach (var o in serializedObject.targetObjects)
                    {
                        ((Volume) o).ChangeCollisionMode((CollisionMode) collisionMode.enumValueIndex);
                    }
                }
                if (collisionMode.enumValueIndex > 0)
                {
                    collisionTrigger.boolValue = EditorGUILayout.ToggleLeft(new GUIContent(" Is Trigger"),
                        collisionTrigger.boolValue);
                    if (collisionTrigger.boolValue != voxelObject.CollisionTrigger)
                    {
                        foreach (var o in serializedObject.targetObjects)
                        {
                            ((Volume) o).CollisionTrigger = collisionTrigger.boolValue;
                            ((Volume) o).CreateChunks();
                        }
                    }

                    physicMaterial.objectReferenceValue = EditorGUILayout.ObjectField("Physic Material: ",
                        physicMaterial.objectReferenceValue,
                        typeof (PhysicMaterial),
                        false);
                    if (physicMaterial.objectReferenceValue != voxelObject.PhysicMaterial)
                    {
                        foreach (var o in serializedObject.targetObjects)
                        {
                            ((Volume) o).PhysicMaterial = (PhysicMaterial) physicMaterial.objectReferenceValue;
                            ((Volume) o).CreateChunks();
                        }
                    }

                    separateColliderMesh.boolValue =
                        EditorGUILayout.ToggleLeft(
                            new GUIContent(" Generate collider mesh separately (Edit-time only)"),
                            separateColliderMesh.boolValue);
                    if (separateColliderMesh.boolValue != voxelObject.GenerateMeshColliderSeparately)
                    {
                        foreach (var o in serializedObject.targetObjects)
                        {
                            ((Volume) o).GenerateMeshColliderSeparately = separateColliderMesh.boolValue;
                            ((Volume) o).CreateChunks();
                        }
                    }

                    if (separateColliderMesh.boolValue)
                    {
                        EditorUtility.SkinnedLabel("Collider Meshing Mode");
                        colliderMeshingMode.enumValueIndex =
                            Convert.ToInt16(EditorGUILayout.EnumPopup((MeshingMode) colliderMeshingMode.enumValueIndex));
                        if (colliderMeshingMode.enumValueIndex != Convert.ToInt16(voxelObject.MeshColliderMeshingMode))
                        {
                            foreach (var o in serializedObject.targetObjects)
                            {
                                ((Volume) o).MeshColliderMeshingMode = (MeshingMode) colliderMeshingMode.enumValueIndex;
                                ((Volume) o).CreateChunks();
                            }
                        }
                    }
                }

                EditorGUILayout.Space();

                EditorUtility.SkinnedLabel("Meshing Mode");
                meshingMode.enumValueIndex =
                    Convert.ToInt16(EditorGUILayout.EnumPopup((MeshingMode) meshingMode.enumValueIndex));
                if (meshingMode.enumValueIndex != Convert.ToInt16(voxelObject.MeshingMode))
                {
                    foreach (var o in serializedObject.targetObjects)
                    {
                        ((Volume) o).MeshingMode = (MeshingMode) meshingMode.enumValueIndex;
                        ((Volume) o).CreateChunks();
                    }
                }

                EditorUtility.SkinnedLabel("Mesh Compression");
                meshCompression.enumValueIndex =
                    Convert.ToInt16(
                        EditorGUILayout.EnumPopup((ModelImporterMeshCompression) meshCompression.enumValueIndex));
                if (meshCompression.enumValueIndex != Convert.ToInt16(voxelObject.MeshCompression))
                {
                    foreach (var o in serializedObject.targetObjects)
                    {
                        ((Volume) o).MeshCompression = (ModelImporterMeshCompression) meshCompression.enumValueIndex;
                        ((Volume) o).CreateChunks();
                    }
                }

                selfShadeInt.floatValue = EditorGUILayout.Slider("Self-Shading Intensity", selfShadeInt.floatValue, 0, 1);
                if (selfShadeInt.floatValue != voxelObject.SelfShadingIntensity)
                {
                    foreach (var o in serializedObject.targetObjects)
                    {
                        ((Volume) o).SelfShadingIntensity = selfShadeInt.floatValue;
                        ((Volume) o).UpdateAllChunks();
                    }
                }

                material.objectReferenceValue = EditorGUILayout.ObjectField("Material: ", material.objectReferenceValue,
                    typeof (Material),
                    false);
                if (material.objectReferenceValue != voxelObject.Material)
                {
                    foreach (var o in serializedObject.targetObjects)
                    {
                        ((Volume) o).Material = (Material) material.objectReferenceValue;
                        ((Volume) o).CreateChunks();
                    }
                }

                castShadows.enumValueIndex =
                    Convert.ToInt16(EditorGUILayout.EnumPopup("Cast Shadows",
                        (ShadowCastingMode) castShadows.enumValueIndex));
                if (castShadows.enumValueIndex != Convert.ToInt16(voxelObject.CastShadows))
                {
                    foreach (var o in serializedObject.targetObjects)
                    {
                        ((Volume) o).CastShadows = (ShadowCastingMode) castShadows.enumValueIndex;
                        ((Volume) o).CreateChunks();
                    }
                }

                receiveShadows.boolValue = EditorGUILayout.ToggleLeft(new GUIContent(" Receive Shadows"),
                    receiveShadows.boolValue);
                if (receiveShadows.boolValue != voxelObject.ReceiveShadows)
                {
                    foreach (var o in serializedObject.targetObjects)
                    {
                        ((Volume) o).ReceiveShadows = receiveShadows.boolValue;
                        ((Volume) o).CreateChunks();
                    }
                }
            }




            EditorGUILayout.Space();
            EditorGUILayout.Space();
            if (GUILayout.Button(new GUIContent("Mesh-Only Copy", "Create a copy of this Volume with mesh(es) only")))
            {
                foreach (var target in serializedObject.targetObjects)
                {

                    GameObject bake = ((Volume) Instantiate(target)).gameObject;
                    bake.GetComponent<Volume>().AssetGuid = Guid.NewGuid().ToString();
                    foreach (Frame f in bake.GetComponent<Volume>().Frames)
                        f.AssetGuid = Guid.NewGuid().ToString();
                    bake.GetComponent<Volume>().CreateChunks();
                    bake.tag = "Untagged";
                    bake.name = target.name + " (Copy)";
                    DestroyImmediate(bake.GetComponent<Volume>());
                    DestroyImmediate(bake.transform.FindChild("Hitbox").gameObject);
                    for (int i = 0; i < bake.transform.childCount; i++)
                    {
                        GameObject o = bake.transform.GetChild(i).gameObject;
                        if (o.GetComponent<Frame>() != null)
                        {
                            DestroyImmediate(o.GetComponent<Frame>());
                            for (int c = 0; c < o.transform.FindChild("Chunks").childCount; c++)
                            {
                                GameObject chunk = o.transform.FindChild("Chunks").GetChild(c).gameObject;
                                if (chunk.GetComponent<Chunk>() != null)
                                    DestroyImmediate(chunk.GetComponent<Chunk>());
                            }
                        }
                    }

                }
            }

            EditorGUILayout.Space();
            if (GUILayout.Button(new GUIContent("Refresh Chunks", "Regenerates all chunk meshes for all frames")))
            {
                foreach (var o in serializedObject.targetObjects)
                {
                    ((Volume) o).CreateChunks();
                }
            }

            if (voxelObject.ImportedFrom != Importer.None && !string.IsNullOrEmpty(voxelObject.ImportedFile) &&
                serializedObject.targetObjects.Length < 2 && !voxelObject.IsEnabledForEditing)
            {
                EditorGUILayout.Space();
                if (
                    GUILayout.Button(
                        new GUIContent(
                            voxelObject.ImportedFrom == Importer.Magica
                                ? "Re-import from MagicaVoxel"
                                : "Re-import from Image",
                            voxelObject.ImportedFrom == Importer.Magica
                                ? "Re-import from original .VOX file"
                                : "Re-import from original image file")))
                {
                    if (UnityEditor.EditorUtility.DisplayDialog("Warning!",
                        "Re-importing will overwrite any changes made since original import. This cannot be undone!",
                        "OK", "Cancel"))
                    {
                        foreach (var o in serializedObject.targetObjects)
                        {
                            switch (voxelObject.ImportedFrom)
                            {
                                case Importer.Magica:
                                    MagicaVoxelImporter.MagicaVoxelImport((Volume) o);
                                    break;
                                case Importer.Image:
                                    ImageImporter.ImageImport((Volume) o);
                                    break;
                            }
                        }
                    }
                }
            }

            if (Application.isPlaying)
            {
                EditorGUILayout.Space();
                if (GUILayout.Button(new GUIContent("Rebuild Volume", "Reset any voxels that have been destroyed")))
                {
                    foreach (var o in serializedObject.targetObjects)
                    {
                        ((Volume) o).Rebuild();
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
