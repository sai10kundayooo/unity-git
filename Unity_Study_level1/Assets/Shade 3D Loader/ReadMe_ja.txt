
Shade 3D Loader

Unity プロジェクトに追加されたShade 3D ファイル(.shd)を自動的にUnityで使用できる形式(FBX)にコンバートするスクリプトです。
コンバートされたFBXはプロジェクト内のShade 3D ファイルが更新された場合に自動的に更新されるため、Shade 3D ファイルをネイティブファイルのように扱うことができます。
このスクリプトは、Shade 3D for Unity　または Shade 13.2.1 以降がインストールされている必要があります。
Shade 3D for Unity は Mac App Store で無料で入手することができます。

Shade 3D for Unity (Mac App Store)
 https://itunes.apple.com/jp/app/shade-3d-for-unity/id569369787?mt=12

Shade 13
 http://www.shade13.jp/


使用方法：

1. Unityにて、プロジェクトを作成するか、既存のプロジェクトを開きます。
2. Unityで開いたプロジェクトの Projectビューに、Asset Storeから "Shade 3D Loader" をインポートします。これでスクリプトのインストールは完了です。
3. Shadeのシーンファイルを、Projectビューにドラッグアンドドロップします。
4. スクリプトが自動で起動し、.shdファイルをFBXファイルに変換します。
（この時、自動的にShadeがサイレントに起動し、shdファイルをFBXファイルへと変換します）
5. 変換されたファイルは、"shdファイル名.shd.fbx" という名称のファイルとして生成されます。
6. プロジェクト内のshdファイルが更新されると、次にUnityのウインドウに切り替わるタイミングで自動的にFBXファイルが再生成されます。

※ Projectビュー内で、右クリック > Reveal in Finder... メニューを実行すると、現在作業しているプロジェクトのAssetフォルダが開きます。
このフォルダに直接shdファイルをコピーする事も可能です。この場合、次にUnityをアクティブにしたタイミングで再読込されます。


重要なお知らせ：

※ Shade 3D Loader は Shade 13 体験版では動作しません。
※ Shade 3D Loader は Shade 13.2 以前のバージョンには対応していません。13.2.1 以降にアップデートしてください。
※ Windowsにて複数の Shade がインストールされている場合は最後にインストールされたShadeが使用されます。特定のShadeを使用するように変更したい場合は、Shadeのインストーラでインストールすることの出来る「関連づけツール」を使って、.shdファイルの関連づけを切り換えてください。
