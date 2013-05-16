// Version 1.1.0 - Copyright 2012 e frontier, Inc. All Rights Reserved.

/**
 * Shade 3D Loader.js
 * @author e frontier, Inc.
 */

#pragma strict

import System.IO;
import System.Diagnostics;
import System.Text.RegularExpressions;

//function Start () {
//}

//function Update () {
//}

class ShadeToUnityPostprocessor extends AssetPostprocessor {
    function GetVersion () { return 1100; }
    function OnPreprocessModel () {
        //UnityEngine.Debug.Log("OnPreprocessModel: " + assetPath);
        if (assetPath.Contains(".shd.fbx")) {
            var labels = AssetDatabase.GetLabels(assetImporter);
            if (!ArrayUtility.Contains(labels, "Shade 3D Loader")) {
                //UnityEngine.Debug.Log("Do default processing behavior");
                var modelImporter = assetImporter as ModelImporter;
                modelImporter.materialName = ModelImporterMaterialName.BasedOnModelNameAndMaterialName;
                // Ignore importFBX Warnings: Can't import normals, because mesh '' doesn't have it.
                modelImporter.normalImportMode = ModelImporterTangentSpaceMode.Calculate;
            }
        }
    }
//    function OnPreprocessTexture () {
//        //UnityEngine.Debug.Log("OnPreprocessTexture: " + assetPath);
//        //if (assetPath.Contains(".shd.fbx")) {
//        //    var textureImporter = assetImporter as TextureImporter;
//        //    textureImporter.convertToNormalmap = true;
//        //}
//    }
//    function OnAssignMaterialModel (material : Material, renderer : Renderer) : Material {
//        //UnityEngine.Debug.Log("OnAssignMaterialModel: name:" + material.name);
//        //var materialPath = "Assets/" + material.name + ".mat";
//
//        // Find if there is a material at the material path
//        // Turn this off to always regeneration materials
//        //if (AssetDatabase.LoadAssetAtPath(materialPath, typeof(Material)))
//        //    return AssetDatabase.LoadAssetAtPath(materialPath, typeof(Material));
//
//        // Create a new material asset using the specular shader
//        // but otherwise the default values from the model
//        //material.shader = Shader.Find("Specular");
//        //AssetDatabase.CreateAsset(material, "Assets/" + material.name + ".mat");
//        //return material;
//        return null;
//    }
    static function GetShadePath () {
        if (Application.platform == RuntimePlatform.OSXEditor) {
            var appPath =  "/Applications/Shade 3D for Unity.app/Contents/MacOS/xshade";
            if (CheckShade(appPath, false)) return appPath;
            appPath =  "/Applications/Shade 13E.app/Contents/MacOS/xshade";
            if (CheckShade(appPath, false)) return appPath;
            appPath =  "/Applications/Shade 13.app/Contents/MacOS/xshade";
            if (CheckShade(appPath, false)) return appPath;

            var st = new System.Diagnostics.StackTrace(true);
            var currentFile = st.GetFrame(0).GetFileName();
            var scriptPath = Path.GetDirectoryName(currentFile) + '/Tools/shdexttool';

            var newProcess = new Process();
            newProcess.StartInfo.FileName = "chmod";
            newProcess.StartInfo.Arguments = "+x \"" + scriptPath + "\"";
            newProcess.StartInfo.UseShellExecute = false;
            newProcess.Start();
            newProcess.WaitForExit();

            newProcess.StartInfo.FileName = scriptPath;
            newProcess.StartInfo.Arguments = "shd";
            newProcess.StartInfo.RedirectStandardOutput = true;
            newProcess.StartInfo.RedirectStandardError = true;
            newProcess.Start();
            var stdout = newProcess.StandardOutput.ReadToEnd();
            var stderr = newProcess.StandardError.ReadToEnd();
            newProcess.WaitForExit();
            appPath = stdout.Replace("\n", "").Replace("%20", " ").Replace("file://localhost", "") + "Contents/MacOS/xshade";
            //UnityEngine.Debug.LogWarning(appPath);
            return appPath;
        }
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            var regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\e-frontier, Inc.\\Shade 3D for Unity");
            var exe_path = "";
            if (regKey) {
                exe_path = regKey.GetValue("exe_path") as String;
                regKey.Close();
                if (exe_path && CheckShade(exe_path, false)) return exe_path;
            }
            regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\e-frontier, Inc.\\Shade 3D for Unity");
            if (regKey) {
                exe_path = regKey.GetValue("exe_path") as String;
                regKey.Close();
                if (exe_path && CheckShade(exe_path, false)) return exe_path;
            }
            regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(".shd");
            if (regKey) {
                var fileType = regKey.GetValue("") as String;
                regKey.Close();
                regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(fileType + "\\shell\\open\\command");
                if (regKey) {
                    var command = regKey.GetValue("") as String;
                    regKey.Close();
                    exe_path = command.Replace("\"", "").Replace("%1", "");
                    if (exe_path && CheckShade(exe_path, false)) return exe_path;
                }
            }
            return exe_path;
        }
        return "";
    }
    static var disableNoGUI:boolean = false;
    static function GetShadeArgs (scriptPath) {
        if (Application.platform == RuntimePlatform.OSXEditor) {
            if (disableNoGUI) { return "--gui --run=\"" + scriptPath + "\""; }
            else { return "--nogui --run=\"" + scriptPath + "\""; }
        }
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            if (disableNoGUI) { return "/gui /run=\"" + scriptPath + "\""; }
            else { return "/nogui /run=\"" + scriptPath + "\""; }
        }
        return "";
    }
    static function GetSandboxDir () {
        if (Application.platform == RuntimePlatform.OSXEditor) {
            var homePath = System.Environment.GetEnvironmentVariable("HOME");
            return homePath + "/Library/Containers/com.e-frontier.shade3dgame/Data/Library/Application Support/e frontier/Shade 3D Loader";
        }
        return "";
    }
    static function CheckSandbox () {
        if (Application.platform == RuntimePlatform.OSXEditor) {
              var shadePath = GetShadePath() as String;
            if (File.Exists(shadePath)) {
                var newProcess = new Process();
                newProcess.StartInfo.FileName = "/usr/bin/codesign";
                newProcess.StartInfo.UseShellExecute = false;
                newProcess.StartInfo.RedirectStandardOutput = true;
                newProcess.StartInfo.RedirectStandardError = true;
                newProcess.StartInfo.Arguments = "--display --entitlements - \"" + shadePath + "\"";
                newProcess.Start();
                var stdout = newProcess.StandardOutput.ReadToEnd();
                var stderr = newProcess.StandardError.ReadToEnd();
                newProcess.WaitForExit();
                var result = newProcess.ExitCode;
                if (result == 0 && stdout != "") {
                    return true;
                }
            }
            return false;
        }
        return false;
    }
    static function GetTempDir () {
        if (CheckSandbox()) return GetSandboxDir();
        if (System.Environment.GetEnvironmentVariable("TMPDIR")) { return System.Environment.GetEnvironmentVariable("TMPDIR"); }
        if (System.Environment.GetEnvironmentVariable("TMP")) { return System.Environment.GetEnvironmentVariable("TMP"); }
        return System.Environment.GetEnvironmentVariable("TEMP");
    }
    static function CheckShade (shadePath:String, showLog:boolean) {
        //UnityEngine.Debug.Log("Shade 3D Loader: " + shadePath);
        if (File.Exists(shadePath)) {
            var newProcess = new Process();
            newProcess.StartInfo.FileName = shadePath;
            newProcess.StartInfo.UseShellExecute = false;
            newProcess.StartInfo.RedirectStandardOutput = true;
            if (Application.platform == RuntimePlatform.OSXEditor) { newProcess.StartInfo.Arguments = "--nogui --version --help"; }
            if (Application.platform == RuntimePlatform.WindowsEditor) { newProcess.StartInfo.Arguments = "/nogui /version /help"; }
            newProcess.Start();
            var output = newProcess.StandardOutput.ReadToEnd();
            newProcess.WaitForExit();
            //UnityEngine.Debug.Log("Shade 3D Loader: " + output);
            var match = Regex.Match(output, "Shade (([0-9]+[.])?([0-9]+[.])?[0-9]+) (.*)");
            if (match.Success) {
                var version = match.Groups[1].Value;
                var grade = match.Groups[4].Value;
                //UnityEngine.Debug.Log("Shade 3D Loader: " + version);
                //UnityEngine.Debug.Log("Shade 3D Loader: " + grade);
                match = Regex.Match(output, "Build version ([0-9]+)");
                if (match.Success) {
                    var buildnumber = parseInt(match.Groups[1].Value as String);
                    if (buildnumber < 462000) {
                        if (showLog) {
                            UnityEngine.Debug.Log("Shade 3D Loader: " + shadePath);
                            UnityEngine.Debug.LogError("Shade 3D Loader: Unsupported version of Shade 3D application found.");
                        }
                        return false;
                    }
                    if (buildnumber == 463042) { // 13.2 MAS
                        if (showLog) {
                            UnityEngine.Debug.Log("Shade 3D Loader: " + shadePath);
                            UnityEngine.Debug.LogWarning("Shade 3D Loader: This version of Shade 3D for Unity (13.2.0) has animation export problem. The newer version is recommended.");
                        }
                        return true;
                    }
                    if (buildnumber == 463050) { // 13.2
                        if (showLog) {
                            UnityEngine.Debug.Log("Shade 3D Loader: " + shadePath);
                            UnityEngine.Debug.LogWarning("Shade 3D Loader: This version of Shade 3D application is not supported. Please update to the latest version 13.2.1 or above.");
                        }
                        //disableNoGUI = true;
                        return false;
                    }
                    if (grade.Contains("DEMO")) {
                        if (showLog) {
                            UnityEngine.Debug.Log("Shade 3D Loader: " + shadePath);
                            UnityEngine.Debug.LogError("Shade 3D Loader: Trial version of Shade 3D is not supported. Please make sure to activate your copy of Shade by input a correct serial number.");
                        }
                        return false;
                    }
                    return true;
                }
            }
            if (showLog) {
                UnityEngine.Debug.Log("Shade 3D Loader: " + shadePath);
                if (Application.platform == RuntimePlatform.OSXEditor) {
                    UnityEngine.Debug.LogError("Shade 3D Loader: Unsupported version of Shade 3D application found. To use Shade 3D loader, please download the latest Shade 3D for Unity from Mac App Store or purchase Shade 13 from e frontier or Mirye store.");
                } else {
                    UnityEngine.Debug.LogError("Shade 3D Loader: Unsupported version of Shade 3D application found. To use Shade 3D loader, please purchase Shade 13 from e frontier or Mirye store.");
                }
            }
            return false;
        }
        if (showLog) UnityEngine.Debug.LogError("Shade 3D Loader: Cannot find Shade 3D application. Please re-install it again.");
        return false;
    }
    static function OnPostprocessAllAssets (
        importedAssets : String[],
        deletedAssets : String[],
        movedAssets : String[],
        movedFromAssetPaths : String[]) {
        for (var str in importedAssets) {
            if (Path.GetExtension(str) == ".shd") {
                //UnityEngine.Debug.Log("Reimported Asset: " + str);
                var shadePath = GetShadePath() as String;
                if (CheckShade(shadePath, true)) {
                    var parentFolder = Path.GetDirectoryName(str);
                    var outputFolder = parentFolder;
                    var fileName =  Path.GetFileName(str);
                    var newFileName = fileName;
                    var shdPath = Path.GetDirectoryName(Application.dataPath) + "/" + str;
                    var dstPath = outputFolder + "/" + newFileName + ".fbx";
                    //UnityEngine.Debug.Log(dstPath);
                    if (!File.Exists(dstPath) || 0 < System.DateTime.Compare(File.GetLastWriteTime(shdPath), File.GetLastWriteTime(dstPath))) {
                        //UnityEngine.Debug.Log("Reimported Asset: " + shdPath);
                        //var tempPath = Path.GetDirectoryName(Application.dataPath) + "/Temp/ShadeConverted.shd.fbx";
                        var tempPath = GetTempDir() + "/" + FileUtil.GetUniqueTempPathInProject() + "/" + newFileName + ".fbx";
                        if (File.Exists(tempPath)) FileUtil.DeleteFileOrDirectory(tempPath);
                        System.Environment.SetEnvironmentVariable('SHADETOUNITY_OUTPUT_PATH', tempPath);
                        var st = new System.Diagnostics.StackTrace(true);
                        var currentFile = st.GetFrame(0).GetFileName();
                        var scriptPath = Path.GetDirectoryName(currentFile) + '/Tools/ShadeToUnity.py';
                        //UnityEngine.Debug.Log(scriptPath);
                        if (CheckSandbox()) {
                            var sandboxPath = GetSandboxDir();
                            if (!File.Exists(sandboxPath)) Directory.CreateDirectory(sandboxPath);
                            var sandboxScriptPath = sandboxPath + "/ShadeToUnity.py";
                            if (File.Exists(sandboxScriptPath)) FileUtil.DeleteFileOrDirectory(sandboxScriptPath);
                            FileUtil.CopyFileOrDirectory(scriptPath, sandboxScriptPath);
                            scriptPath = sandboxScriptPath;
                            //UnityEngine.Debug.Log(scriptPath);
                            var sandboxShdPath = sandboxPath + "/" + Path.GetFileName(str);
                            if (File.Exists(sandboxShdPath)) FileUtil.DeleteFileOrDirectory(sandboxShdPath);
                            FileUtil.CopyFileOrDirectory(shdPath, sandboxShdPath);
                            shdPath = sandboxShdPath;
                            //UnityEngine.Debug.Log(shdPath);
                        }
                        var args = '"' + shdPath + '" ' + GetShadeArgs(scriptPath);
                        //UnityEngine.Debug.LogWarning(args);
                        var newProcess = new Process();
                        newProcess.StartInfo.FileName = shadePath;
                        newProcess.StartInfo.UseShellExecute = false;
                        newProcess.StartInfo.RedirectStandardOutput = true;
                        newProcess.StartInfo.RedirectStandardError = true;
                        newProcess.StartInfo.Arguments = args;
                        newProcess.Start();
                        var stdout = newProcess.StandardOutput.ReadToEnd();
                        var stderr = newProcess.StandardError.ReadToEnd();
                        newProcess.WaitForExit();
                        if (stdout != '') UnityEngine.Debug.LogWarning(stdout);
                        //UnityEngine.Debug.Log(stderr);
                        //if (!File.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);
                        FileUtil.ReplaceFile(tempPath, dstPath);
                        AssetDatabase.ImportAsset(dstPath);
                        if (File.Exists(tempPath)) FileUtil.DeleteFileOrDirectory(tempPath);
                        //if (Application.platform == RuntimePlatform.OSXEditor) {
                        //    if (File.Exists(sandboxScriptPath)) FileUtil.DeleteFileOrDirectory(sandboxScriptPath);
                        //    if (File.Exists(sandboxShdPath)) FileUtil.DeleteFileOrDirectory(sandboxShdPath);
                        //}
                    }
                }
            }
            else if (str.Contains(".shd.fbx")) {
                //UnityEngine.Debug.Log("Reimported Asset: " + str);
                var object = AssetDatabase.LoadAssetAtPath(str, typeof(GameObject));
                var labels = AssetDatabase.GetLabels(object);
                if (!ArrayUtility.Contains(labels, "Shade 3D Loader")) {
                    //UnityEngine.Debug.Log("Do default processing behavior");
                    var newLabels = new Array(labels);
                    newLabels.Push("Shade 3D Loader");
                    AssetDatabase.SetLabels(object, newLabels.ToBuiltin(String) as String[]);
                }
            }
        }
        //for (var str in deletedAssets) {
        //    //UnityEngine.Debug.Log("Deleted Asset: " + str);
        //    if (Path.GetExtension(str) == ".shd") {
        //        //UnityEngine.Debug.Log("Deleted Asset: " + str);
        //    }
        //}
        //for (var i=0;i<movedAssets.Length;i++) {
        //    //UnityEngine.Debug.Log("Moved Asset: " + movedAssets[i] + " from: " + movedFromAssetPaths[i]);
        //    if (Path.GetExtension(movedAssets[i]) == ".shd") {
        //        //UnityEngine.Debug.Log("Moved Asset: " + movedAssets[i] + " from: " + movedFromAssetPaths[i]);
        //    }
        //}
    }
}
