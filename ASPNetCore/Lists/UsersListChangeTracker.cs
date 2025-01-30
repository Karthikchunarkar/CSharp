namespace lists ;
 using classes; using d3e.core; using gqltosql2; using io.reactivex.rxjava3.disposables; using io.reactivex.rxjava3.functions; using java.util.stream; using lists; using models; using rest.ws; using store;  public class UsersListChangeTracker :  Cancellable {  private class OrderBy { public string OrderByValue0 
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
 }  private class UsersListInput { private int pageSize 
 private int offset 
 private string orderBy 
 private bool ascending 
 public UsersListInput (  UsersListRequest _req ) {
 this.pageSize = _req.PageSize() ;
 this.offset = _req.Offset() ;
 this.orderBy = _req.OrderBy() ;
 this.ascending = _req.IsAscending() ;
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
 public void Load (  DataChangeTracker tracker ) {
 }
 public void Unload (  ) {
 }
 } private UsersList root ;
 
 private List < OrderBy > orderBy = ListExt.List() ;
 
 private DataChangeTracker tracker ;
 
 private ClientSession clientSession ;
 
 private java.util.List < Disposable > disposables = ListExt.List() ;
 ;
 
 private UsersListInput inputs ;
 
 private Field field ;
 
 public UsersListChangeTracker (  DataChangeTracker tracker, ClientSession clientSession, Field field ) {
 this.tracker = tracker ;
 this.clientSession = clientSession ;
 this.field = field ;
 }
 public void Init (  OutObject out, UsersList initialData, UsersListRequest inputs ) {
 {
  BaseUser currentUser=CurrentUser.Get();
 if ( !(currentUser is not Admin) ) {
 throw new Exception("Current user type does not have read permissions for this ObjectList.") ;
 }
 }
 initialData.ClearChanges() ;
 this.inputs = new UsersListInput(inputs) ;
 StoreInitialData(initialData) ;
 AddSubscriptions() ;
 out.setId(root.getId()) ;
 disposables.Add(tracker.listen(out,field,clientSession)) ;
 }
 public void Dispose (  ) {
 disposables.forEach((  d ) => d.dispose()) ;
 }
 private void StoreInitialData (  UsersList initialData ) {
 root = initialData ;
 orderBy = initialData.getItems().stream().map((  x ) => new OrderBy(null,x.getId())).collect(Collectors.toList()) ;
  long id=IdGenerator.getNext();
 root = new setId(id) ;
 initialData._convertToObjectRef() ;
 }
 private void AddSubscriptions (  ) {
 // This method will register listeners on each reference that is referred to in the DataQuery expression.
 // A listener is added by default on the Table from which we pull the data, since any change in that must trigger a subscription change.
  Disposable baseSubscribe=tracker.listen(SchemaConstants.Admin,null,(  obj, old, type ) => applyAdmin(((Admin)obj),((Admin)obj),type));
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
 public void applyAdmin (  Admin model, Admin old, StoreEventType type ) {
 if ( type == StoreEventType.Insert ) {
 // New data is inserted, So we just insert the new data depending on the clauses.
 CreateInsertChange(model) ;
 }
 else if ( type == StoreEventType.Delete ) {
 // Existing data is deleted
 CreateDeleteChange(model) ;
 }
 else {
 // Existing data is updated
 CreatePathChangeChange(model,old) ;
 }
 }
 private void CreateInsertChange (  Admin model ) {
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
 private void CreateDeleteChange (  Admin model ) {
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
 private bool CreatePathChangeChange (  Admin model, Admin old ) {
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
 private void moveItems (  Admin model, TypeAndId data, string OrderByValue0 ) {
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