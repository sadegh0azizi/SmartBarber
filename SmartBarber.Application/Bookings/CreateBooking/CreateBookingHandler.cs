using SmartBarber.Application.Abstraction;
using SmartBarber.Application.Abstraction.Repositories;
using SmartBarber.Application.Common.Errors;
using SmartBarber.Application.Common.Results;
using SmartBarber.Domain.Entities;
using SmartBarber.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBarber.Application.Bookings.CreateBooking
{
    public class CreateBookingHandler
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookingHandler(IBookingRepository bookingRepository, IUnitOfWork unitOfWork)
        {
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
        }
       

        public async Task<CreateBookingResult> HandleAsync(CreateBookingCommand command)
        {

            return await _unitOfWork.ExecuteInTransactionAsync<CreateBookingResult>(async() =>
            {
                var hasConflict =
               await _bookingRepository.HasConflictAsync(command.ProviderId, command.Date, command.TimeRange);

                if (hasConflict)
                    return new CreateBookingResult(Common.Results.ApplicationResultStatus.Failed, null, ApplicationError.BookingConflict);

                var booking = new Booking(command.CustomerId, command.ServiceId, command.ProviderId, command.Date, command.TimeRange, command.DepositAmount);
                await _bookingRepository.AddAsync(booking);
                await _unitOfWork.SaveChangesAsync();

                return new CreateBookingResult(
                            ApplicationResultStatus.Success,
                            booking.Id,
                            null);
            });


            

        }
    }
}
    