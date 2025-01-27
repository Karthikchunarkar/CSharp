namespace helpers ;
 using DefaultUser = models.DefaultUser; using EntityMutator = store.EntityMutator; using QueryProvider = d3e.core.QueryProvider;  public class DefaultUserSingletonHelper { public static DefaultUserSingletonHelper instance 
 private EntityMutator mutator 
 private DefaultUser defaultUser 
 public void init (  ) {
 instance = this ;
 }
 public void set (  DefaultUser obj ) {
 this.defaultUser = obj ;
 }
 public static DefaultUserSingletonHelper get (  ) {
 if ( instance.defaultUser == null ) {
 instance.defaultUser = QueryProvider.get().getDefaultUser() ;
 }
 return instance ;
 }
 }