using GigHubCore.Core.Dtos;
using GigHubCore.Core.IRepositories;
using GigHubCore.Core.Models;
using GigHubCore.Persistence.DbContexts;
using System;
using System.Linq;

namespace GigHubCore.Persistence.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeleteAttend(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }

        public void AttendGig(AttendanceDto dto, string userId)
        {
            var attendace = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };
            _context.Attendances.Add(attendace);
        }

        public ILookup<int, Attendance> GetFutureAttendances(string userId, int gigId)
        {
            var futureAttendances = _context.Attendances
                            .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                            .ToList()
                            .ToLookup(a => a.GigId);

            //se não existir presenças para esse show
            if (!futureAttendances.Contains(gigId))
                throw new Exception();
            else
                return futureAttendances;
        }

        public ILookup<int, Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                            .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                            .ToList()
                            .ToLookup(a => a.GigId);
        }

        public Attendance GetAttendance(int gigId, string userId)
        {
            return _context.Attendances.SingleOrDefault(a => a.GigId == gigId && a.AttendeeId == userId);
        }

    }
}
