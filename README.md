# ExportHumanoidRig

## 概要

これはアバターの Humanoid リグとボーンの対応を JSON ファイルにエクスポートする Unity エディタ拡張です。


## インストール

[リリースページ](https://github.com/Taremin/ExportHumanoidRig/releases)から最新のバージョンをダウンロードして、解凍したものをアセット内にコピーします。


### インストール時の注意

ここで注意するのは `Editor` フォルダも **そのまま** コピーすることです。

これはUnityの仕様で「`Editor`フォルダの中にあるスクリプトはエディターでのみ有効で、ゲーム実行時には無視される」というのがあるからです。
(参考: [特殊なフォルダー名 - Unity マニュアル](https://docs.unity3d.com/ja/2018.4/Manual/SpecialFolders.html))

`Editor` フォルダ内の `*.cs` ファイルのみをアセットにいれてしまうと、ゲーム実行時にも実行されてしまいエラーが発生します。


## 使い方

1. ヒエラルキーでエクスポートしたいオブジェクトを選択
2. ヒエラルキーで右クリックしてコンテキストメニューから `ExportHumanoidRig` をクリック
3. `ExportHumanoidRig` ウィンドウが開くので `GameObject` にエクスポートしたいアバター(ゲームオブジェクト)を選択します
   - ヒエラルキーからのドラッグアンドドロップ、あるいは右の `◎` みたいなアイコンから選択
4. `Export` ボタンを押す

エクスポートに成功すると、Assetsフォルダ直下に `[ゲームオブジェクト名].json` というファイルが作成されています。

## ライセンス

[MIT](./LICENSE)