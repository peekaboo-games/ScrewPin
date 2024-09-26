using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ETypeScrew
{
    NONE = -1,
    SCREW_1,
    SCREW_2,
    SCREW_3,
    SCREW_4,
    SCREW_5,
}
public class ScrewController : MonoBehaviour
{
    [SerializeField] private DataSoScrew _dataSOScrew;
    [SerializeField] private List<ScrewItem> _listScrews;
    public DataSoScrew DataSOScrew;
  

}
