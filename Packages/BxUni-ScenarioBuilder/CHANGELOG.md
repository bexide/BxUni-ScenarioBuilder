# CHANGELOG

## [v1.1.0](2025/11/05)
[Fixed]
- Unity6を使用している場合は、`FindObjectsOfType`を使っている所を`FindObjectsByType(FindObjectSortMode.None)`に変更

[Changed]
- CommandRegistConfigアセットで設定していたTooltipをAttributeで設定できるようにする`CommandTooltipAttribute`の追加
    - 両方設定している場合はCommandRegistConfigの方が適用されます。
- コマンドの表示テキストを変えるだけならEditor拡張をせずにできるように、BaseCommandに`GetDefaultText()`メソッドを追加

[Added]
- シナリオ再生中に別のシナリオアセットを割り込みで呼ぶ機能の追加
    - 挙動確認用にサンプルを追加しました。

## [v1.0.5](2024/05/20)
[Fixed]
- BaseCommandを継承したクラスをさらに継承したクラスがEditorWindow上で設定できない問題の修正

[Added]
- プロジェクト内のシナリオファイルの中から、指定のコマンドが入っているかを検索するウィンドウ「ScenarioCommandWindow」を追加
- Homeへの期間の簡易化 [#1](https://github.com/bexide/BxUni-ScenarioBuilder/pull/1)
- R3対応

## [v1.0.4] (2023/09/14)
[Fixed]
- Editorウィンドウの軽微な不具合修正
[Added]
- シナリオファイルをダブルクリックして編集ウィンドウを開けるように修正

## [v1.0.3] (2023/08/26)
[Fixed]
- Editorウィンドウの軽微な不具合修正

## [v1.0.2] (2023/08/26)
[Changed]
- 編集ウィンドウの最適化、軽量化

## [v1.0.1] (2023/01/22)
[Fixed]
- Editorウィンドウが起動できない問題を修正

## [v1.0.0] (2022/12/12)
[Added]
- リリースver1.0.0