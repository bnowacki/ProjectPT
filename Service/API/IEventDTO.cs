﻿namespace Service.API;

public interface IEventDTO
{
    int Id { get; set; }

    int stateId { get; set; }

    int userId { get; set; }

    DateTime occurrenceDate { get; set; }

    string Type { get; set; }

    int? Quantity { get; set; }
}
