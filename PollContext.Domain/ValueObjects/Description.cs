﻿using Flunt.Validations;
using PollContext.Shared.ValueObjects;
using System.Collections.Generic;

namespace PollContext.Domain.ValueObjects
{
    public class DescriptionVO : ValueObject
    {


        public DescriptionVO(string description)
        {
            Description = description;

            AddNotifications(
                new Contract()
                .Requires()
                .IsNotNullOrEmpty(Description, "DescriptionVO.Description", "Descrição é obrigatória")
                .HasMinLen(Description, 3, "DescriptionVO.Description", "Descrição deve conter ao menos 3 caracteres.")
                .HasMaxLen(Description, 150, "DescriptionVO.Description", "Descrição não pode ter mais do que 150 caracteres."));

        }

        public DescriptionVO()
        {
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Description;
        }
        public string Description { get; private set; }
    }
}
