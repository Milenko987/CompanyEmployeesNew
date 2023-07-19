namespace Entities.Exceptions
{
    public class MaxAgeRangeBadRequestExcepetion : BadRequestException
    {
        public MaxAgeRangeBadRequestExcepetion() : base("Max age can not be less than min age") { }
    }
}
