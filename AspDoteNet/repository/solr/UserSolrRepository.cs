namespace repository.solr ;
  public interface UserSolrRepository :  org.springframework.data.solr.repository.SolrCrudRepository < models.User,Long > { }