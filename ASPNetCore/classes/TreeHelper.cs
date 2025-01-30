namespace classes ;
 using classes;  public class TreeHelper { public TreeHelper (  ) {
 }
 public static < T > void expandLast (  List<T> result, OneFunction<T,Iterable<T>> expand ) {
 foreach ( T i in expand.apply(result.getLast()) ) {
 result.add(i) ;
 TreeHelper.expandLast(result,expand) ;
 }
 }
 public static < T > List<T> expandAll (  List<T> baseValue, OneFunction<T,Iterable<T>> expand ) {
  List<T> result=null;
 foreach ( T i in baseValue ) {
 result.add(i) ;
 TreeHelper.expandLast(result,expand) ;
 }
 return result ;
 }
 public static < T,R > R doOn (  T value, OneFunction<T,R> fun ) {
 return fun.apply(value) ;
 }
 public static < T > void expandAndSearchLast (  string searchText, List<T> result, T next, OneFunction<T,Iterable<T>> expand, OneFunction<T,string> toStringProvider ) {
 foreach ( T i in expand.apply(next) ) {
 if ( searchText.equals("") || toStringProvider.apply(i).toLowerCase().contains(searchText.toLowerCase(),0) ) {
 result.add(i) ;
 }
 TreeHelper.expandAndSearchLast(searchText,result,i,expand,toStringProvider) ;
 }
 }
 public static < T > List<T> expandAndSearchAll (  string searchText, List<T> baseValue, OneFunction<T,Iterable<T>> expand, OneFunction<T,string> toStringProvider ) {
  List<T> result=null;
 foreach ( T i in baseValue ) {
 if ( searchText.equals("") || toStringProvider.apply(i).toLowerCase().contains(searchText.toLowerCase(),0) ) {
 result.add(i) ;
 }
 TreeHelper.expandAndSearchLast(searchText,result,i,expand,toStringProvider) ;
 }
 return result ;
 }
 }