namespace EFInterceptor.Model;

internal record OutboxMessage(Guid Id, string EntityType, string Content, DateTime CreatedOn);