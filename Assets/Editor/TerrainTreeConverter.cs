using UnityEngine;
using UnityEditor; // Required to use MenuItem and PrefabUtility

public class TerrainTreeConverter : MonoBehaviour
{
    [MenuItem("Tools/Convert Terrain Trees to GameObjects")]
    static void Convert()
    {
        // Get the active terrain in your scene
        Terrain terrain = Terrain.activeTerrain;
        if (terrain == null)
        {
            Debug.LogError("No active terrain found!");
            return;
        }

        // Get terrain data (this includes tree instances and prototypes)
        TerrainData terrainData = terrain.terrainData;

        // Loop through every tree instance (each one corresponds to a placed tree)
        foreach (TreeInstance tree in terrainData.treeInstances)
        {
            // Get the prefab used for this tree
            GameObject prefab = terrainData.treePrototypes[tree.prototypeIndex].prefab;

            // Convert terrain-space position (normalized 0–1) to world coordinates
            Vector3 worldPos = Vector3.Scale(tree.position, terrainData.size) + terrain.transform.position;
            worldPos.y = terrain.SampleHeight(worldPos) + terrain.transform.position.y;

            // Instantiate a new GameObject version of the tree prefab
            GameObject newTree = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

            newTree.transform.position = worldPos;
            newTree.transform.rotation = Quaternion.identity;

            // Add Rigidbody if not already present
            if (!newTree.GetComponent<Rigidbody>())
                newTree.AddComponent<Rigidbody>();

            // Add a Collider if missing (adjust type as needed)
            if (!newTree.GetComponent<Collider>())
                newTree.AddComponent<CapsuleCollider>();
        }

        Debug.Log("✅ Converted all terrain trees to GameObjects with Rigidbody and Collider.");
    }
}
