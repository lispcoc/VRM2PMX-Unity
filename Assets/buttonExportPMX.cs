using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class buttonExportPMX : MonoBehaviour
{
    public Text msg;

    /// ボタンをクリックした時の処理
    public void OnClick() {
        if ( buttonImportVRM.instance == null) {
            msg.text = "Error: VRM File is not loaded.";
        } else {
            try {
                buttonImportVRM.instance.GetComponent< PMXExporter >().Init();
                msg.text = "Export PMX finished successfully.";
            } catch(Exception ex) {
                msg.text = "Error: " + ex.Message;
            }
        }
    }

}
