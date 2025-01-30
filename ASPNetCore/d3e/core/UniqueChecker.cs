namespace d3e.core ;
 using d3e.core; using models;  public class UniqueChecker { public static bool checkEmailUniqueInAdmin (  Admin admin, string email ) {
 return QueryProvider.get().checkEmailUniqueInAdmin(admin.getId(),email) ;
 }
 public static bool checkTokenUniqueInOneTimePassword (  OneTimePassword oneTimePassword, string token ) {
 return QueryProvider.get().checkTokenUniqueInOneTimePassword(oneTimePassword.getId(),token) ;
 }
 }