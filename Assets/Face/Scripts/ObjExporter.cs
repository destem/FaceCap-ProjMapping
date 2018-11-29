using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class ObjExporter {

	public static string MeshToString(Vector3[] verts, Vector2[] uvs, int[] faces) {
		//Mesh m = mf.mesh;
		//Material[] mats = mf.renderer.sharedMaterials;

		StringBuilder sb = new StringBuilder();

		sb.Append("g ").Append("derp").Append("\n");
		foreach(Vector3 v in verts) {
			sb.Append(string.Format("v {0} {1} {2}\n",v.x,v.y,v.z));
		}
		//sb.Append("\n");
		//foreach(Vector3 v in m.normals) {
		//	sb.Append(string.Format("vn {0} {1} {2}\n",v.x,v.y,v.z));
		//}
		sb.Append("\n");
		foreach(Vector2 v in uvs) {
			sb.Append(string.Format("vt {0} {1}\n",v.x,v.y));
		}
		sb.Append ("\n");
		//for (int material=0; material < m.subMeshCount; material ++) {
		//	sb.Append("\n");
		//	sb.Append("usemtl ").Append(mats[material].name).Append("\n");
		//	sb.Append("usemap ").Append(mats[material].name).Append("\n");

			int[] triangles = faces;
			for (int i=0;i<triangles.Length;i+=3) {
				sb.Append(string.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n", 
					triangles[i]+1, triangles[i+1]+1, triangles[i+2]+1));
			}
		//}
		return sb.ToString();
	}

	public static void MeshToFile(Vector3[] verts, Vector2[] uvs, int[] faces, string filename) {
		using (StreamWriter sw = new StreamWriter(filename)) 
		{
			sw.Write(MeshToString(verts, uvs, faces));
		}
	}
}