namespace repository.solr ;
  public interface CategorySolrRepository :  org.springframework.data.solr.repository.SolrCrudRepository < models.Category,Long > { }