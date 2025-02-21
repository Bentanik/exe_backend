namespace exe_backend.Application.Persistence.Repository;

public interface ILectureRepository : IRepositoryBase<Lecture, Guid>
{
    Task<ICollection<Lecture>> GetLecturesNotChapterAsync(Guid[] lectureIds);
}