# BxUni Scenario Builder 「External UniTask」

---

## UniTask SUPPORT

本パッケージを導入しているプロジェクトにUniTaskを導入することで  
「CommandEngineDirector」のコンポーネントで使用している一部メソッド及びイベントが変更されます。  

```csharp

[SerializeField] CommandEngineDirector m_director;

//onPostResetTask イベントでカプセル化されるメソッドの戻り値が
//System.Threading.Tasks.Task から
//Cysharp.Threading.Tasks.UniTaskに変更されます。
m_director.onPostResetTask += async () => { };

//PlayTask メソッドの返値が
//System.Threading.Tasks.Task から
//Cysharp.Threading.Tasks.UniTaskに変更されます。
await m_director.PlayTask();

```

## 依存ライブラリ
[UniTask](https://github.com/Cysharp/UniTask)