# BxUni Scenario Builder 「External UniRx」

---

## UniRx SUPPORT

本パッケージを導入しているプロジェクトにUniRxを導入することで  
「CommandEngineDirector」のコンポーネントで使用可能なプロパティが増えます。  

```csharp
[SerializeField] CommandEngineDirector m_director;

m_director.OnStart(_ => 
{
    // シナリオ再生時に呼ばれます。
    // m_director.onStartイベントにコールバックを登録する方法と
    // 同等の機能となります。
}).AddTo(this);

m_director.OnEnd(_ => 
{
    // シナリオ再生終了時に呼ばれます。
    // m_director.onEndイベントにコールバックを登録する方法と
    // 同等の機能となります。
}).AddTo(this);

m_director.OnResetCompleted(_ => 
{
    // シナリオ再生時に呼ばれます。
    // m_director.onResetCompletedイベントにコールバックを登録する方法と
    // 同等の機能となります。
}).AddTo(this);

```

## 依存ライブラリ
[UniRx](https://github.com/neuecc/UniRx)