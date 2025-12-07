using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using comp231_002__Team1_TeamUp_SportsBooking.Data;
using comp231_002__Team1_TeamUp_SportsBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace comp231_002__Team1_TeamUp_SportsBooking.Services
{
    public class NotificationService
    {
        private readonly ApplicationDbContext _context;

        public NotificationService(ApplicationDbContext context)
        {
            _context = context;
        }

        // ===============================
        // CREATE NOTIFICATION
        // ===============================
        public async Task CreateNotificationAsync(string userId, string message, int? bookingId = null)
        {
            var notification = new Notification
            {
                UserId = userId,
                Message = message,
                BookingID = bookingId,     // <--- supports cascade delete
                CreatedAt = DateTime.Now
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        // ===============================
        // GET ALL NOTIFICATIONS FOR USER
        // ===============================
        public async Task<List<Notification>> GetNotificationsForUserAsync(string userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId || userId == "system")  // allow system notifications
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        // ===============================
        // GET ALL NOTIFICATIONS (for public bell)
        // ===============================
        public async Task<List<Notification>> GetAllNotificationsAsync()
        {
            return await _context.Notifications
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }
    }
}
