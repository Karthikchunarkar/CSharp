namespace rest.ws ;
 using d3e.core; using models;  public static class PermissionCheckUtil { public static bool CanCreate (  int userTypeIdx, int obCTypeIdx ) {
 switch ( userTypeIdx ) { case SchemaConstants.Admin: {
  var user=((Admin)CurrentUser.Get());
 switch ( obCTypeIdx ) { case SchemaConstants.OneTimePassword: {
 return true ;
 }
 case SchemaConstants.AnonymousUser: {
 return true ;
 }
 case SchemaConstants.ChangePasswordRequest: {
 return true ;
 }
 case SchemaConstants.EmailMessage: {
 return true ;
 }
 case SchemaConstants.Admin: {
 return true ;
 }
 case SchemaConstants.Category: {
 return true ;
 }
 case SchemaConstants.Event: {
 return true ;
 }
 case SchemaConstants.BulkEvent: {
 return true ;
 }
 } break; }
 case SchemaConstants.AnonymousUser: {
  var user=((AnonymousUser)CurrentUser.Get());
 switch ( obCTypeIdx ) { case SchemaConstants.AnonymousUser: {
 return true ;
 }
 case SchemaConstants.ChangePasswordRequest: {
 return true ;
 }
 case SchemaConstants.SignUpRequest: {
 return true ;
 }
 case SchemaConstants.EmailMessage: {
 return true ;
 }
 case SchemaConstants.OneTimePassword: {
 return true ;
 }
 case SchemaConstants.PushNotification: {
 return true ;
 }
 case SchemaConstants.SMSMessage: {
 return true ;
 }
 case SchemaConstants.VerificationData: {
 return true ;
 }
 case SchemaConstants.Admin: {
 return true ;
 }
 case SchemaConstants.Event: {
 return true ;
 }
 } break; }
 } return false ;
 }
 public static bool CanUpdate (  int userTypeIdx, int obCTypeIdx ) {
 switch ( userTypeIdx ) { case SchemaConstants.Admin: {
  var user=((Admin)CurrentUser.Get());
 switch ( obCTypeIdx ) { case SchemaConstants.OneTimePassword: {
 return true ;
 }
 case SchemaConstants.AnonymousUser: {
 return true ;
 }
 case SchemaConstants.ChangePasswordRequest: {
 return true ;
 }
 case SchemaConstants.EmailMessage: {
 return true ;
 }
 case SchemaConstants.Admin: {
 return true ;
 }
 case SchemaConstants.Category: {
 return true ;
 }
 case SchemaConstants.Event: {
 return true ;
 }
 case SchemaConstants.BulkEvent: {
 return true ;
 }
 } break; }
 case SchemaConstants.AnonymousUser: {
  var user=((AnonymousUser)CurrentUser.Get());
 switch ( obCTypeIdx ) { case SchemaConstants.AnonymousUser: {
 return true ;
 }
 case SchemaConstants.ChangePasswordRequest: {
 return true ;
 }
 case SchemaConstants.EmailMessage: {
 return true ;
 }
 case SchemaConstants.OneTimePassword: {
 return true ;
 }
 case SchemaConstants.PushNotification: {
 return true ;
 }
 case SchemaConstants.SMSMessage: {
 return true ;
 }
 case SchemaConstants.VerificationData: {
 return true ;
 }
 case SchemaConstants.Event: {
 return true ;
 }
 } break; }
 } return false ;
 }
 public static bool CanDelete (  int userTypeIdx, int obCTypeIdx ) {
 switch ( userTypeIdx ) { case SchemaConstants.Admin: {
  var user=((Admin)CurrentUser.Get());
 switch ( obCTypeIdx ) { case SchemaConstants.AnonymousUser: {
 return true ;
 }
 case SchemaConstants.EmailMessage: {
 return true ;
 }
 case SchemaConstants.Admin: {
 return true ;
 }
 case SchemaConstants.Category: {
 return true ;
 }
 case SchemaConstants.Event: {
 return true ;
 }
 case SchemaConstants.BulkEvent: {
 return true ;
 }
 } break; }
 case SchemaConstants.AnonymousUser: {
  var user=((AnonymousUser)CurrentUser.Get());
 switch ( obCTypeIdx ) { case SchemaConstants.AnonymousUser: {
 return true ;
 }
 case SchemaConstants.ChangePasswordRequest: {
 return true ;
 }
 case SchemaConstants.EmailMessage: {
 return true ;
 }
 case SchemaConstants.OneTimePassword: {
 return true ;
 }
 case SchemaConstants.PushNotification: {
 return true ;
 }
 case SchemaConstants.SMSMessage: {
 return true ;
 }
 case SchemaConstants.VerificationData: {
 return true ;
 }
 case SchemaConstants.Event: {
 return true ;
 }
 } break; }
 } return false ;
 }
 public static bool CanReadDataQuery (  int userTypeIdx, int dqTypeIdx ) {
 switch ( userTypeIdx ) { case SchemaConstants.Admin: {
 switch ( dqTypeIdx ) { case SchemaConstants.EventsList: {
 return true ;
 }
 case SchemaConstants.UsersList: {
 return true ;
 }
 case SchemaConstants.CategoriesList: {
 return true ;
 }
 case SchemaConstants.CalendarEventList: {
 return true ;
 }
 case SchemaConstants.ReviewEventList: {
 return true ;
 }
 } }
 case SchemaConstants.AnonymousUser: {
 switch ( dqTypeIdx ) { case SchemaConstants.EventsList: {
 return true ;
 }
 case SchemaConstants.CalendarEventList: {
 return true ;
 }
 case SchemaConstants.CategoriesList: {
 return true ;
 }
 } }
 } return false ;
 }
 }