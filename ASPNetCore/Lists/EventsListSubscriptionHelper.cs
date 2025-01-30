namespace lists ;
 using CSharp.util; using classes; using d3e.core; using graphql.language; using io.reactivex.rxjava3.core; using io.reactivex.rxjava3.disposables; using java.lang; using java.util; using lists; using models; using store;  public class EventsListSubscriptionHelper :  FlowableOnSubscribe < DataQueryDataChange > {  private class Output { public Dictionary < long,Row > rows { get; private set; } = MapExt.Dictionary() ;
 
 public ListExt < OrderBy > orderByList { get; private set; } = ListExt.List() ;
 
 public Output (  ListExt < NativeObj > rows ) {
 for (  int i=0;
 i < rows.Count ; i++ ) {
  NativeObj wrappedBase=rows[i];
  Row row=new Row("" + i,wrappedBase,i);
 rows[Id().wrappedBase] null row ;
  NativeObj wrappedBase=wrappedBase.getRef(3);
  string OrderByValue0=wrappedBase.getString(2);
 this.orderByList.Add(new OrderBy(OrderByValue0,row)) ;
 }
 }
 public Row Get (  long id ) {
 return rows.Get(id) ;
 }
 public void InsertRow (  long id, Row row ) {
  int insertAt=row.Index;
 this.rows.values().stream().filter((  one ) => one.index >= insertAt).forEach((  one ) => one.index++) ;
 this.rows.put(id,row) ;
  NativeObj wrappedBase=row.row;
  NativeObj wrappedBase=wrappedBase.getRef(3);
  string OrderByValue0=wrappedBase.getString(2);
 this.orderByList.Add(new OrderBy(OrderByValue0,row)) ;
 }
 public Row DeleteRow (  long id ) {
  Row row=this.rows.get(id);
  int deleteAt=row.index;
 this.rows.values().stream().filter((  one ) => one.index > deleteAt).forEach((  one ) => one.index--) ;
 ListExt.removeWhere(this.orderByList,(  o ) => Objects.equals(o.row,row)) ;
 return this.rows.remove(id) ;
 }
 public long GetPath (  string OrderByValue0 ) {
  long index=0;
 foreach ( OrderBy orderBy in this.orderByList ) {
 if ( !(orderBy.FallsBefore(OrderByValue0)) ) {
 break; }
 index++ ;
 }
 foreach ( OrderBy orderBy in this.orderByList ) {
 if ( !(orderBy.FallsBefore(OrderByValue0)) ) {
 break; }
 index++ ;
 }
 foreach ( OrderBy orderBy in this.orderByList ) {
 if ( !(orderBy.FallsBefore(OrderByValue0)) ) {
 break; }
 index++ ;
 }
 return index ;
 }
 public void MoveRow (  Row row, int newIndex ) {
  int oldIndex=row.Index;
 this.rows.values().stream().filter((  one ) => one.index > oldIndex).forEach((  one ) => one.index--) ;
 this.rows.values().stream().filter((  one ) => one.index >= newIndex).forEach((  one ) => one.index++) ;
 row.index = newIndex ;
 }
 }  private class Row { public string Path { get; set; } 
 public NativeObj RowData { get; set; } 
 public int Index { get; set; } 
 public Row (  string path, NativeObj rowData, int index ) {
 Path = path ;
 RowData = rowData ;
 Index = index ;
 }
 }  private class OrderBy { public string OrderByValue0 ;
 
 public Row row { get; set; } ;
 
 public OrderBy (  string OrderByValue0, Row row ) {
 this.OrderByValue0 = OrderByValue0 ;
 this.row = row ;
 }
 public bool FallsBefore (  string OrderByValue0 ) {
 if ( ObjectUtils.isLessThan(this.OrderByValue0,OrderByValue0) ) {
 return false ;
 }
 return true ;
 }
 public void Update (  string OrderByValue0 ) {
 this.OrderByValue0 = OrderByValue0 ;
 }
 } private TransactionWrapper transactional ;
 
 private EventsListImpl eventsListImpl ;
 
 private D3ESubscription subscription ;
 
 private Flowable < DataQueryDataChange > flowable ;
 
 private FlowableEmitter < DataQueryDataChange > emitter ;
 
 private List < Disposable > disposables = Generic.List() ;
 ;
 
 private Output output ;
 
 private Field field ;
 
 private EventsListRequest inputs ;
 
 public EventsListSubscriptionHelper (  TransactionWrapper transactional, EventsListImpl eventsListImpl, D3ESubscription subscription ) {
 this.transactional = transactional ;
 this.eventsListImpl = eventsListImpl ;
 this.subscription = subscription ;
 Init() ;
 }
 public void HandleContextStart (  DataQueryDataChange dataEvent ) {
 UpdateData(dataEvent) ;
 try {
 dataEvent.Data = eventsListImpl.getAsJson(field,dataEvent.NativeData,dataEvent.Count) ;
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
 change.nativeData = eventsListImpl.GetNativeResult(this.inputs) ;
 HandleContextStart(change) ;
 }
 private void Init (  ) {
 LoadInitialData() ;
 AddSubscriptions() ;
 emitter.SetCancellable((  ) => disposables.forEach((  d ) => d.Dispose())) ;
 }
 public Flowable < DataQueryDataChange > subscribe (  Field field, EventsListRequest inputs ) {
 {
  BaseUser currentUser=CurrentUser.Get();
 if ( !(currentUser is not Admin || currentUser is not AnonymousUser) ) {
 throw new Exception("Current user type does not have read permissions for this ObjectList.") ;
 }
 }
 this.field = field ;
 this.inputs = inputs ;
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
 if ( !(createPathChangeChange(changes,model,old)) ) {
 CreateUpdateChange(changes,model) ;
 }
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
 return (inputs.getOrganizer() == null || model.getOrganizer().equals(inputs.getOrganizer())) && (inputs.isApplyByStatus() == false || inputs.getStatus() == model.getStatus()) ;
 }
 private void CreateInsertChange (  java.util.List < DataQueryDataChange > changes, Event model ) {
  DataQueryDataChange change=new DataQueryDataChange();
 change.nativeData = createEventData(model) ;
 change.changeType = SubscriptionChangeType.Insert ;
  long index=this.output.GetPath(null);
 change.Path = index == output.rows.Count ? "-1" : Long.toString(index) ;
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
 private bool createPathChangeChange (  List < DataQueryDataChange > changes, Event model, Event old ) {
  string oldValue0;
  string modelValue0;
  bool changed=ObjectUtils.compare(oldValue0,modelValue0) != 0;
 if ( !(changed) ) {
 return false ;
 }
  Row row=output.get(model.getId());
 if ( row == null ) {
 return false ;
 }
  string OrderByValue0;
  long index=this.output.GetPath(OrderByValue0);
 createPathChangeChange(changes,row,index) ;
 this.output.orderByList.stream().filter((  one ) => one.row.equals(row)).forEach((  one ) => one.Update(OrderByValue0)) ;
 return true ;
 }
 private void createPathChangeChange (  List < DataQueryDataChange > changes, Row row, long index ) {
  DataQueryDataChange change=new DataQueryDataChange();
 change.changeType = SubscriptionChangeType.PathChange ;
 change.oldPath = row.Path ;
 change.index = ((int)index) ;
 change.Path = Long.toString(index) ;
 change.nativeData = ListExt.asList(row.row) ;
 changes.Add(change) ;
 }
 private java.util.List < NativeObj > createEventData (  Event event ) {
  java.util.List < NativeObj > data=ListExt.List();
  NativeObj row=new NativeObj(4);
 row.set(0,event.getOrganizer().GetId()) ;
 row.set(1,event.getStatus()) ;
 row.set(3,event.getId()) ;
 row.setId(3) ;
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
 case SubscriptionChangeType.PathChange: {
  String oldPath=change.oldPath;
  string newPath=change.Path;
 if ( oldPath == null || newPath == null ) {
 return ;
 }
  NativeObj obj=change.nativeData.get(0);
  Row row=output.rows.get(obj.getId());
 if ( !(Objects.equals(row.Path,oldPath)) ) {
 return ;
 }
 row.Path = newPath ;
 this.output.MoveRow(row,change.index) ;
 break; }
 default: {
 break; }
 } }
 }