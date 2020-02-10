using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;
using System;
using System.IO;
using SFB;
using SimpleFileBrowser;
using UnityEngine.UI;

public class buttonImportVRM : MonoBehaviour
{
    public Text msg;
    public static GameObject instance = null;

    /// ボタンをクリックした時の処理
    public void OnClick() {
        // Open file with filter
        var extensions = new [] {
            new ExtensionFilter("VRM Files", "vrm" ),
            new ExtensionFilter("All Files", "*" ),
        };
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true);
        if(paths.Length > 0) {
            var file = paths[0];

            byte[] bytes = FileBrowserHelpers.ReadBytesFromFile( file );
            var context = new VRMImporterContext();
            try {
                context.ParseGlb(bytes);
                msg.text = "Import VRM finished successfully. (" + file + ")";
                var meta = context.ReadMeta(false); //引数をTrueに変えるとサムネイルも読み込みます
                Debug.LogFormat("meta: title:{0}", meta.Title);
                //同期処理で読み込みます
                context.Load();
                //読込が完了するとcontext.RootにモデルのGameObjectが入っています
                Destroy(instance);
                instance = context.Root;
                instance.transform.position = new Vector3(0, 0, 0);
                instance.transform.rotation = Quaternion.Euler(0, 0, 0);
                instance.AddComponent< PMXExporter >();
                context.ShowMeshes();
            } catch(Exception ex) {
                msg.text = "Error: " + ex.Message;
            }
        }

    }

}
