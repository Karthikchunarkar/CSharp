namespace repository.solr ;
  public interface OneTimePasswordSolrRepository :  org.springframework.data.solr.repository.SolrCrudRepository < models.OneTimePassword,Long > { }