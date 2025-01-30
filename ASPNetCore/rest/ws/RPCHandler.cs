namespace rest.ws ;
 using classes; using d3e.core; using models; using org.springframework.stereotype; using rest.ws; Service public class RPCHandler { public void handle (  int clsIdx, int methodIdx, RocketInputContext ctx, rest.ws.RocketMessage msg ) {
 switch ( clsIdx ) { case RPCConstants.UniqueChecker: {
 handleUniqueChecker(methodIdx,ctx,msg) ;
 break; }
 case RPCConstants.FileService: {
 handleFileService(methodIdx,ctx,msg) ;
 break; }
 case RPCConstants.ExportEventService: {
 handleExportEventService(methodIdx,ctx,msg) ;
 break; }
 } }
 private void handleUniqueChecker (  int methodIdx, RocketInputContext ctx, rest.ws.RocketMessage msg ) {
 switch ( methodIdx ) { case RPCConstants.UniqueCheckerCheckTokenUniqueInOneTimePassword: {
  OneTimePassword oneTimePassword=ctx.ReadObject();
  string token=ctx.ReadString();
  bool result=UniqueChecker.checkTokenUniqueInOneTimePassword(oneTimePassword,token);
 msg.writeByte(0) ;
 ctx.WriteBool(result) ;
 break; }
 case RPCConstants.UniqueCheckerCheckEmailUniqueInAdmin: {
  Admin admin=ctx.ReadObject();
  string email=ctx.ReadString();
  bool result=UniqueChecker.checkEmailUniqueInAdmin(admin,email);
 msg.writeByte(0) ;
 ctx.WriteBool(result) ;
 break; }
 } }
 private void handleFileService (  int methodIdx, RocketInputContext ctx, rest.ws.RocketMessage msg ) {
 switch ( methodIdx ) { case RPCConstants.FileServiceCreateTempFile: {
  string fullNameOrExtn=ctx.ReadString();
  bool extnGiven=ctx.ReadBool();
  string content=ctx.ReadString();
  DFile result=FileService.createTempFile(fullNameOrExtn,extnGiven,content);
 msg.writeByte(0) ;
 ctx.WriteDFile(result) ;
 break; }
 } }
 private void handleExportEventService (  int methodIdx, RocketInputContext ctx, rest.ws.RocketMessage msg ) {
 switch ( methodIdx ) { case RPCConstants.ExportEventServiceExportURL: {
  List<string> categories=ctx.ReadStringCollection();
  string result=ExportEventService.exportURL(categories);
 msg.writeByte(0) ;
 ctx.WriteString(result) ;
 break; }
 } }
 }