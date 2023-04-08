using System.Runtime.Serialization;
using AutoMapper;
using clar2.Application.Common.Mappings;
using clar2.Application.Common.Models;
using clar2.Application.TodoLists.Queries.GetTodos;
using clar2.Domain.ToDoItems;
using clar2.Domain.TodoLists;
using NUnit.Framework;

namespace clar2.Application.UnitTests.Common.Mappings;

public class MappingTests {
  private readonly IConfigurationProvider _configuration;
  private readonly IMapper _mapper;

  public MappingTests() {
    _configuration = new MapperConfiguration(config =>
      config.AddProfile<MappingProfile>());

    _mapper = _configuration.CreateMapper();
  }

  [Test]
  public void ShouldHaveValidConfiguration() {
    _configuration.AssertConfigurationIsValid();
  }

  [Test]
  [TestCase(typeof(TodoList), typeof(TodoListDto))]
  [TestCase(typeof(TodoItem), typeof(TodoItemDto))]
  [TestCase(typeof(TodoList), typeof(LookupDto))]
  [TestCase(typeof(TodoItem), typeof(LookupDto))]
  public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination) {
    var instance = GetInstanceOf(source);

    _mapper.Map(instance, source, destination);
  }

  private object GetInstanceOf(Type type) {
    if (type.GetConstructor(Type.EmptyTypes) != null)
      return Activator.CreateInstance(type)!;

    // Type without parameterless constructor
    return FormatterServices.GetUninitializedObject(type);
  }
}
