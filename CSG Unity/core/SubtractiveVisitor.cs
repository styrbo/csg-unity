
namespace CSG
{
	class SubtractiveVisitor : CsgVisitor
	{

		public void ProcessMaster(CsgOperation inOperation, Face inFace, CsgOperation.EPolySide inSide,
			CsgOperation.OperationInfo info)
		{
			switch (inSide)
			{
				case CsgOperation.EPolySide.PolySide_Outside:
				case CsgOperation.EPolySide.PolySide_Planar_Outside:
				case CsgOperation.EPolySide.PolySide_CoPlanar_Inside:
					// add cutted polygons
					if ((inFace.flags & Face.FaceFlags_WasCutted) != 0)
						inOperation.AddPlanarFace(inFace);
					break;
				case CsgOperation.EPolySide.PolySide_Inside:
				case CsgOperation.EPolySide.PolySide_Planar_Inside:
				case CsgOperation.EPolySide.PolySide_CoPlanar_Outside:
					// discard node
					inOperation.MarkNodeAsDestroyed();
					break;
			}

		}

		public void ProcessSlave(CsgOperation inOperation, Face inFace, CsgOperation.EPolySide inSide,
			CsgOperation.OperationInfo info)
		{
			switch (inSide)
			{
				case CsgOperation.EPolySide.PolySide_Outside:
				case CsgOperation.EPolySide.PolySide_Planar_Outside:
				case CsgOperation.EPolySide.PolySide_CoPlanar_Outside:
				case CsgOperation.EPolySide.PolySide_CoPlanar_Inside:
					break;
				case CsgOperation.EPolySide.PolySide_Inside:
				case CsgOperation.EPolySide.PolySide_Planar_Inside:
					// clone face
					Face newFace = (Face) inFace.Clone();
					newFace.Reverse();
					// add to deferred faces
					inOperation.AddDeferredFace(newFace);
					break;
			}
		}
	}
}