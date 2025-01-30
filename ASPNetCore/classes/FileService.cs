namespace classes ;
 using classes; using d3e.core;  public class FileService { public FileService (  ) {
 }
 public static DFile createTempFile (  string fullNameOrExtn, bool extnGiven, string content ) {
 return FileHelper.get().createTempFile(fullNameOrExtn,extnGiven,content) ;
 }
 }