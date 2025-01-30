namespace lists ;
 using CSharp.util; using classes; using d3e.core; using graphql.language; using io.reactivex.rxjava3.core; using io.reactivex.rxjava3.disposables; using java.lang; using lists; using models; using store;  public class CalendarEventListSubscriptionHelper :  FlowableOnSubscribe < DataQueryDataChange > {  private class Output { public Dictionary < long,Row > rows { get; private set; } = MapExt.Dictionary() ;
 
 public Output (  ListExt < NativeObj > rows ) {
 for (  int i=0;
 i < rows.Count ; i++ ) {
  NativeObj wrappedBase=rows[i];
  Row row=new Row("" + i,wrappedBase,i);
 rows[Id().wrappedBase] null row ;
 }
 }
 public Row Get (  long id ) {
 return rows.Get(id) ;
 }
 public void InsertRow (  long id, Row row ) {
  int insertAt=row.Index;
 this.rows.values().stream().filter((  one ) => one.index >= insertAt).forEach((  one ) => one.index++) ;
 this.rows.put(id,row) ;
 }
 public Row DeleteRow (  long id ) {
  Row row=this.rows.get(id);
  int deleteAt=row.index;
 this.rows.values().stream().filter((  one ) => one.index > deleteAt).forEach((  one ) => one.index--) ;
 return this.rows.remove(id) ;
 }
 }  private class Row { public string Path { get; set; } 
 public NativeObj RowData { get; set; } 
 public int Index { get; set; } 
 public Row (  string path, NativeObj rowData, int index ) {
 Path = path ;
 RowData = rowData ;
 Index = index ;
 }
 } private TransactionWrapper transactional ;
 
 private CalendarEventListImpl calendarEventListImpl ;
 
 private D3ESubscription subscription ;
 
 private Flowable < DataQueryDataChange > flowable ;
 
 private FlowableEmitter < DataQueryDataChange > emitter ;
 
 private List < Disposable > disposables = Generic.List() ;
 ;
 
 private Output output ;
 
 private Field field ;
 
 public CalendarEventListSubscriptionHelper (  TransactionWrapper transactional, CalendarEventListImpl calendarEventListImpl, D3ESubscription subscription ) {
 this.transactional = transactional ;
 this.calendarEventListImpl = calendarEventListImpl ;
 this.subscription = subscription ;
 Init() ;
 }
 public void HandleContextStart (  DataQueryDataChange dataEvent ) {
 UpdateData(dataEvent) ;
 try {
 dataEvent.Data = calendarEventListImpl.getAsJson(field,dataEvent.NativeData,dataEvent.Count) ;
 dataEvent.NativeData = null ;
 }
 catch ( Exception e ) {
 throw new Exception(e) ;
 }
 this.emitter.onNext(dataEvent) ;
 }
 public void Subscribe (  FlowableEmitter < DataQueryDataChange > emitter ) {
 this.emitter = emitter ;
 transactional.DoInTransaction(Init) ;
 }
 private void LoadInitialData (  ) {
  DataQueryDataChange change=new DataQueryDataChange();
 change.changeType = SubscriptionChangeType.All ;
 change.nativeData = calendarEventListImpl.GetNativeResult() ;
 HandleContextStart(change) ;
 }
 private void Init (  ) {
 LoadInitialData() ;
 AddSubscriptions() ;
 emitter.SetCancellable((  ) => disposables.forEach((  d ) => d.Dispose())) ;
 }
 public Flowable < DataQueryDataChange > subscribe (  Field field ) {
 {
  BaseUser currentUser=CurrentUser.Get();
 if ( !(currentUser is not Admin || currentUser is not AnonymousUser) ) {
 throw new Exception("Current user type does not have read permissions for this ObjectList.") ;
 }
 }
 this.field = field ;
 this.flowable = Flowable.create(this,BackpressureStrategy.BUFFER) ;
 return this.flowable ;
 }
 private void AddSubscriptions (  ) {
 // This method will register listeners on each reference that is referred to in the DataQuery expression.
 // A listener is added by default on the Table from which we pull the data, since any change in that must trigger a subscription change.
  Disposable baseSubscribe=((Flowable < D3ESubscriptionEvent<Event> >)subscription.onEventChangeEvent()).Subscribe((  e ) => ApplyEvent(e));
 disposables.Add(baseSubscribe) ;
 }
 public void ApplyEvent (  D3ESubscriptionEvent < Event > e ) {
  System.Collections.Generic.List < DataQueryDataChange > changes=ListExt.List();
  Event model=e.model;
  StoreEventType type=e.changeType;
 if ( type == StoreEventType.Insert ) {
 // New data is inserted, So we just insert the new data depending on the clauses.
 if ( ApplyWhere(model) ) {
 CreateInsertChange(changes,model) ;
 }
 }
 else if ( type == StoreEventType.Delete ) {
 // Existing data is deleted
 CreateDeleteChange(changes,model) ;
 }
 else {
 // Existing data is updated
  Event old=model.GetOld();
 if ( old == null ) {
 return ;
 }
  bool currentMatch=ApplyWhere(model);
  bool oldMatch=ApplyWhere(old);
 if ( currentMatch == oldMatch ) {
 if ( !(currentMatch) && !(oldMatch) ) {
 return ;
 }
 CreateUpdateChange(changes,model) ;
 }
 else {
 if ( oldMatch ) {
 CreateDeleteChange(changes,model) ;
 }
 if ( currentMatch ) {
 CreateInsertChange(changes,model) ;
 }
 }
 }
 PushChanges(changes) ;
 }
 private bool ApplyWhere (  Event model ) {
 return model.getStatus() == EventStatus.Approved ;
 }
 private void CreateInsertChange (  java.util.List < DataQueryDataChange > changes, Event model ) {
  DataQueryDataChange change=new DataQueryDataChange();
 change.nativeData = createEventData(model) ;
 change.changeType = SubscriptionChangeType.Insert ;
 change.Path = "-1" ;
 change.index = output.rows.Count() ;
 changes.Add(change) ;
 }
 private void CreateUpdateChange (  java.util.List < DataQueryDataChange > changes, Event model ) {
  Row row=output.get(model.getId());
 if ( row == null ) {
 return ;
 }
  DataQueryDataChange change=new DataQueryDataChange();
 change.changeType = SubscriptionChangeType.Update ;
 change.Path = row.Path ;
 change.index = row.index ;
 change.nativeData = ListExt.asList(row.row) ;
 changes.Add(change) ;
 }
 private void CreateDeleteChange (  Csharp.util.List < DataQueryDataChange > changes, Event model ) {
  Row row=output.get(model.getId());
 if ( row == null ) {
 return ;
 }
  DataQueryDataChange change=new DataQueryDataChange();
 change.changeType = SubscriptionChangeType.Delete ;
 change.Path = row.Path ;
 change.index = row.index ;
 change.nativeData = ListExt.asList(row.row) ;
 changes.Add(change) ;
 }
 private java.util.List < NativeObj > createEventData (  Event event ) {
  java.util.List < NativeObj > data=ListExt.List();
  NativeObj row=new NativeObj(2);
 row.set(0,event.getStatus()) ;
 row.set(1,event.getId()) ;
 row.setId(1) ;
 data.Add(row) ;
 return data ;
 }
 private void PushChanges (  java.util.List < DataQueryDataChange > changes ) {
 changes.forEach((  one ) => {
 HandleContextStart(one) ;
 }
) ;
 }
 private void UpdateData (  DataQueryDataChange change ) {
 switch ( change.changeType ) { case SubscriptionChangeType.All: {
 this.output = new Output(change.nativeData) ;
 break; }
 case SubscriptionChangeType.Delete: {
  NativeObj del=change.nativeData.get(0);
 output.DeleteRow(del.getId()) ;
 break; }
 case SubscriptionChangeType.Insert: {
  NativeObj add=change.nativeData.get(0);
  String path=change.Path.equals("-1") ? output.rows.Count + "" : change.path;
  Row newRow=new Row(path,add,change.index);
 output.InsertRow(add.getId(),newRow) ;
 break; }
 case SubscriptionChangeType.Update: {
 break; }
 default: {
 break; }
 } }
 }