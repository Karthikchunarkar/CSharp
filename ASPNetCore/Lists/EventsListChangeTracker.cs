namespace lists ;
 using classes; using d3e.core; using gqltosql2; using io.reactivex.rxjava3.disposables; using io.reactivex.rxjava3.functions; using java.util.stream; using lists; using models; using rest.ws; using store;  public class EventsListChangeTracker :  Cancellable {  private class OrderBy { public string OrderByValue0 
 public long Id { get; private set; } 
 public OrderBy (  string OrderByValue0, long id ) {
 OrderByValue0 = OrderByValue0 ;
 id = Id ;
 }
 public bool FallsBefore (  string OrderByValue0 ) {
 if ( ObjectUtils.isLessThan(this.OrderByValue0,OrderByValue0) ) {
 return false ;
 }
 return true ;
 }
 public void Update (  string OrderByValue0 ) {
 OrderByValue0 = OrderByValue0 ;
 }
 }  private class EventsListInput { private int pageSize 
 private int offset 
 private string orderBy 
 private bool ascending 
 private TypeAndId organizerRef 
 private Admin organizer 
 private bool applyByStatus 
 private EventStatus status 
 public EventsListInput (  EventsListRequest _req ) {
 this.pageSize = _req.PageSize() ;
 this.offset = _req.Offset() ;
 this.orderBy = _req.OrderBy() ;
 this.ascending = _req.IsAscending() ;
 this.organizerRef = TypeAndId.From(_req.Organizer()) ;
 this.applyByStatus = _req.IsApplyByStatus() ;
 this.status = _req.Status() ;
 }
 public int PageSize (  ) {
 return pageSize ;
 }
 public int Offset (  ) {
 return offset ;
 }
 public string OrderBy (  ) {
 return orderBy ;
 }
 public bool IsAscending (  ) {
 return ascending ;
 }
 public Admin Organizer (  ) {
 return organizer ;
 }
 public bool IsApplyByStatus (  ) {
 return applyByStatus ;
 }
 public EventStatus Status (  ) {
 return status ;
 }
 public void Load (  DataChangeTracker tracker ) {
 if ( this.organizerRef != null null null ) {
 this.organizer = tracker.FromTypeAndId(this.organizerRef) ;
 }
 }
 public void Unload (  ) {
 this.organizer = null null null ;
 }
 } private EventsList root ;
 
 private List < OrderBy > orderBy = ListExt.List() ;
 
 private DataChangeTracker tracker ;
 
 private ClientSession clientSession ;
 
 private java.util.List < Disposable > disposables = ListExt.List() ;
 ;
 
 private EventsListInput inputs ;
 
 private Field field ;
 
 public EventsListChangeTracker (  DataChangeTracker tracker, ClientSession clientSession, Field field ) {
 this.tracker = tracker ;
 this.clientSession = clientSession ;
 this.field = field ;
 }
 public void Init (  OutObject out, EventsList initialData, EventsListRequest inputs ) {
 {
  BaseUser currentUser=CurrentUser.Get();
 if ( !(currentUser is not Admin || currentUser is not AnonymousUser) ) {
 throw new Exception("Current user type does not have read permissions for this ObjectList.") ;
 }
 }
 initialData.ClearChanges() ;
 this.inputs = new EventsListInput(inputs) ;
 StoreInitialData(initialData) ;
 AddSubscriptions() ;
 out.setId(root.getId()) ;
 disposables.Add(tracker.listen(out,field,clientSession)) ;
 }
 public void Dispose (  ) {
 disposables.forEach((  d ) => d.dispose()) ;
 }
 private void StoreInitialData (  EventsList initialData ) {
 root = initialData ;
 orderBy = initialData.getItems().stream().map((  x ) => new OrderBy(null,x.getId())).collect(Collectors.toList()) ;
  long id=IdGenerator.getNext();
 root = new setId(id) ;
 initialData._convertToObjectRef() ;
 }
 private void AddSubscriptions (  ) {
 // This method will register listeners on each reference that is referred to in the DataQuery expression.
 // A listener is added by default on the Table from which we pull the data, since any change in that must trigger a subscription change.
  Disposable baseSubscribe=tracker.listen(SchemaConstants.Event,null,(  obj, old, type ) => {
 try {
 Inputs.Load(tracker) ;
 applyEvent(((Event)obj),((Event)old),type) ;
 }
 finally {
 Inputs.Unload() ;
 }
 }
);
 Disposables.Add(baseSubscribe) ;
 }
 private TypeAndId Find (  long id ) {
 // TODO: Maybe remove
 return root.GetItemsRef().Stream().Filter((  x ) => x.id == id).FindFirst().orElse(null) ;
 }
 private bool Has (  long id ) {
 return root.GetItemsRef().stream().anyMatch((  x ) => x.id == id) ;
 }
 private void Fire (  ) {
 tracker.Fire(root,StoreEventType.Update) ;
 }
 public void applyEvent (  Event model, Event old, StoreEventType type ) {
 if ( type == StoreEventType.Insert ) {
 // New data is inserted, So we just insert the new data depending on the clauses.
 if ( ApplyWhere(model) ) {
 CreateInsertChange(model) ;
 }
 }
 else if ( type == StoreEventType.Delete ) {
 // Existing data is deleted
 CreateDeleteChange(model) ;
 }
 else {
 // Existing data is updated
  bool currentMatch=ApplyWhere(model);
  bool oldMatch=Has(old.Id);
 if ( currentMatch == oldMatch ) {
 if ( !(currentMatch) && !(oldMatch) ) {
 return ;
 }
 CreatePathChangeChange(model,old) ;
 }
 else {
 if ( oldMatch ) {
 CreateDeleteChange(model) ;
 }
 if ( currentMatch ) {
 CreateInsertChange(model) ;
 }
 }
 }
 }
 private bool ApplyWhere (  Event model ) {
 return (inputs.getOrganizer() == null || model.getOrganizer().equals(inputs.getOrganizer())) && (inputs.isApplyByStatus() == false || inputs.getStatus() == model.getStatus()) ;
 }
 private void CreateInsertChange (  Event model ) {
  long id=model.Id;
  string OrderByValue0;
 // Find index at which to add incoming object
  long index=0;
  int orderBySize=this.orderBy.size();
 while ( index < orderBySize && this.orderBy.get(((int)index)).fallsBefore(OrderByValue0) ) {
 index++ ;
 }
 root.addToItemsRef(new TypeAndId(model._typeIdx(),model.getId()),index) ;
 this.orderBy.Add(((int)index),new OrderBy(OrderByValue0,id)) ;
 root.setCount(root.getCount() + 1) ;
 Fire() ;
 }
 private void CreateDeleteChange (  Event model ) {
  long id=model.getId();
  TypeAndId existing=find(id);
 if ( existing == null ) {
 return ;
 }
 root.removeFromItemsRef(existing) ;
 ListExt.removeWhere(this.orderBy,(  x ) => x.id == id) ;
 root.setCount(root.getCount() - 1) ;
 Fire() ;
 }
 private bool CreatePathChangeChange (  Event model, Event old ) {
  string oldValue0;
  string modelValue0;
  bool changed=ObjectUtils.compare(oldValue0,modelValue0) != 0;
 if ( !(changed) ) {
 return false ;
 }
  long id=model.getId();
  TypeAndId existing=find(id);
 if ( existing == null ) {
 return false ;
 }
  string OrderByValue0;
 moveItems(model,existing,OrderByValue0) ;
 return true ;
 }
 private void moveItems (  Event model, TypeAndId data, string OrderByValue0 ) {
 // Find old index
  int index=this.root.getItemsRef().indexOf(data);
 // Remove from old index
 this.root.removeFromItemsRef(data) ;
  OrderBy toMove=this.orderBy.remove(index);
 // At this point, ref list and orderBy list are balanced
 // Find index at which to add incoming object
  long newIndex=0;
  int orderBySize=this.orderBy.size();
 while ( newIndex < orderBySize && this.orderBy.get(((int)newIndex)).fallsBefore(OrderByValue0) ) {
 newIndex++ ;
 }
  bool append=newIndex == orderBySize;
 if ( append ) {
 // Adding at the end
 newIndex = -1 ;
 }
 this.root.addToItemsRef(data,newIndex) ;
 if ( append ) {
 this.orderBy.Add(toMove) ;
 }
 else {
 this.orderBy.Add(((int)newIndex),toMove) ;
 }
 // Update the moved OrderBy
 toMove.Update(OrderByValue0) ;
 }
 }