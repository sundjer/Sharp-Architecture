﻿namespace SharpArch.NHibernate.NHibernateValidator
{
    using global::NHibernate.Validator.Engine;

    using SharpArch.Domain;
    using SharpArch.Domain.DomainModel;
    using SharpArch.Domain.PersistenceSupport;

    /// <summary>
    ///     Provides a class level validator for determining if the entity has a unique domain signature
    ///     when compared with other entries in the database.
    /// 
    ///     Due to the fact that .NET does not support generic attributes, this only works for entity 
    ///     types having an Id of type int.
    /// </summary>
    public class HasUniqueDomainSignatureValidator : IValidator
    {
        public bool IsValid(object value, IConstraintValidatorContext constraintValidatorContext)
        {
                var entityToValidate = value as IEntityWithTypedId<int>;
            Check.Require(
                entityToValidate != null, 
                "This validator must be used at the class level of an IDomainWithTypedId<int>. The type you provided was " + value.GetType());

            var duplicateChecker = SafeServiceLocator<IEntityDuplicateChecker>.GetService();
            return ! duplicateChecker.DoesDuplicateExistWithTypedIdOf(entityToValidate);
        }
    }
}