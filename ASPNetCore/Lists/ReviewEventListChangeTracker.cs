namespace lists ;
 using classes; using d3e.core; using gqltosql2; using io.reactivex.rxjava3.disposables; using io.reactivex.rxjava3.functions; using java.util; using lists; using models; using rest.ws; using store;  public class ReviewEventListChangeTracker :  Cancellable {  private class ReviewEventListInput { private string referenceID 
 public ReviewEventListInput (  ReviewEventListRequest _req ) {
 this.referenceID = _req.ReferenceID() ;
 }
 public string ReferenceID (  ) {
 return referenceID ;
 }
 public void Load (  DataChangeTracker tracker ) {
 }
 public void Unload (  ) {
 }
 } private ReviewEventList root ;
 
 private DataChangeTracker tracker ;
 
 private ClientSession clientSession ;
 
 private List < Disposable > disposables = ListExt.List() ;
 ;
 
 private ReviewEventListInput inputs ;
 
 private Field field ;
 
 public ReviewEventListChangeTracker (  DataChangeTracker tracker, ClientSession clientSession, Field field ) {
 this.tracker = tracker ;
 this.clientSession = clientSession ;
 this.field = field ;
 }
 public void Init (  OutObject out, ReviewEventList initialData, ReviewEventListRequest inputs ) {
 {
  BaseUser currentUser=CurrentUser.Get();
 if ( !(currentUser is not Admin) ) {
 throw new Exception("Current user type does not have read permissions for this ObjectList.") ;
 }
 }
 initialData.ClearChanges() ;
 this.inputs = new ReviewEventListInput(inputs) ;
 StoreInitialData(initialData) ;
 AddSubscriptions() ;
 out.setId(root.getId()) ;
 disposables.Add(tracker.listen(out,field,clientSession)) ;
 }
 public void Dispose (  ) {
 disposables.forEach((  d ) => d.dispose()) ;
 }
 private void StoreInitialData (  ReviewEventList initialData ) {
 root = initialData ;
  long id=IdGenerator.getNext();
 root = new setId(id) ;
 initialData._convertToObjectRef() ;
 }
 private void AddSubscriptions (  ) {
 // This method will register listeners on each reference that is referred to in the DataQuery expression.
 // A listener is added by default on the Table from which we pull the data, since any change in that must trigger a subscription change.
  Disposable baseSubscribe=tracker.listen(SchemaConstants.Event,null,(  obj, old, type ) => applyEvent(((Event)obj),((Event)obj),type));
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
 return inputs.getReferenceID() == null || model.getReferenceID().equals(inputs.getReferenceID()) ;
 }
 private void CreateInsertChange (  Event model ) {
 root.addToItemsRef(new TypeAndId(model._typeIdx(),model.getId()),-1) ;
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
 root.setCount(root.getCount() - 1) ;
 Fire() ;
 }
 }