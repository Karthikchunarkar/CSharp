namespace classes ;
  public class Result < T > { public ResultStatus Status { get; set; } = ResultStatus.Success ;
 
 public List<string> Errors { get; set; } 
 public T Value { get; set; } 
 public Result (  List<string> errors, ResultStatus status, T value ) {
 this.Errors = errors ;
 this.Status = status ;
 this.Value = value ;
 }
 }