namespace rest.ws ;
 using org.springframework.stereotype; using rest.ws; using store; Service public class Channels :  AbstractChannels { protected AbstractClientProxy getChannelClientProxy (  int idx, ClientSession ses, EntityHelperService helperService, Template template ) {
  AbstractClientProxy proxy=null;
 switch ( idx ) { } return proxy ;
 }
 protected void handleChannelMessage (  int idx, int msgSrvIdx, RocketInputContext ctx, ServerChannel channel ) {
 switch ( idx ) { } }
 }