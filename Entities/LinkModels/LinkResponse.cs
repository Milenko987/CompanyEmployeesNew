using Entities.Models;

namespace Entities.LinkModels
{
    public class LinkResponse
    {
        public bool hasLinks { get; set; }
        public List<ShapedEntity> shapedEntities { get; set; }

        public LinkCollectionWrapper<ShapedEntity> LinkedEntities { get; set; }

        public LinkResponse()
        {
            LinkedEntities = new LinkCollectionWrapper<ShapedEntity>();
            shapedEntities = new List<ShapedEntity>();
        }
    }
}
