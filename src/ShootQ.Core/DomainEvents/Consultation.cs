﻿using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.DomainEvents
{
    public record ConsultationCompleted(DateTime Completed);
    public record ConsultationCreated(Guid ConsultationId, Email ClientEmail, DateRange DateRange);
    public record ConsultationNoteAdded(string Note);
    public record ConsultationPaid(DateTime Paid);
    public record ConsultationRemoved(DateTime Deleted);
    public record ConsultationRescheduled(DateTime StartDate, DateTime EndDate);
}