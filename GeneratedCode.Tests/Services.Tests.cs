using System.Collections.Generic;
using System.Linq;

using GeneratedCode.Tests.HelperFunctions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Website;

namespace GeneratedCode.Tests
{
	[TestClass]
	public class ContactServiceTests
	{
		[TestMethod]
		public void Create()
		{
			// Arrange
			Models.Contact expectedContact = TestData.Contact;

			// Act
			int expectedContactId = ContactService.Create(expectedContact);
			Models.Contact? actualContact = ContactService.Retrieve(expectedContactId);

			// Assert
			if (actualContact != null)
			{
				Assert.AreEqual((object)expectedContactId, actualContact.Value.ContactId, "Unexpected ContactId");
			}

			// Cleanup
			ContactService.Delete(expectedContactId);
		}

		[TestMethod]
		public void Retrieve()
		{
			// Arrange
			Models.Contact expectedContact = TestData.Contact;
			int expectedContactId = ContactService.Create(expectedContact);

			// Act
			Models.Contact? actualContact = ContactService.Retrieve(expectedContactId);

			// Assert
			if (actualContact != null)
			{
				Assert.AreEqual((object)expectedContactId, actualContact.Value.ContactId, "Unexpected ContactId");
			}

			// Cleanup
			ContactService.Delete(expectedContactId);
		}

		[TestMethod]
		public void RetrieveAll()
		{
			// Arrange

			// delete all existing contacts
			foreach (Models.Contact contact in ContactService.RetrieveAll)
			{
				ContactService.Delete(contact.ContactId);
			}

			// set up an array of contacts to create
			Models.Contact[] expectedContacts = new[] { TestData.Contact, TestData.Contact, TestData.Contact, TestData.Contact, TestData.Contact };

			for (int i = 0; i < expectedContacts.Length; i++)
			{
				expectedContacts[i].ContactId = ContactService.Create(expectedContacts[i]);
			}

			// Act
			List<Models.Contact> actualContacts = ContactService.RetrieveAll;

			// Assert
			Assert.AreEqual(expectedContacts.Length, actualContacts.Count, "Unexpected number of records");

			for (int i = 0; i < expectedContacts.Length; i++)
			{
				Assert.AreEqual(expectedContacts[i], actualContacts[i], "Unexpected Contact");
			}

			// Cleanup
			foreach (Models.Contact contact in expectedContacts)
			{
				ContactService.Delete(contact.ContactId);
			}
		}

		[TestMethod]
		public void Update()
		{
			// Arrange
			Models.Contact originalContact = TestData.Contact;
			int originalContactId = ContactService.Create(originalContact);

			Models.Contact updatedContact = TestData.Contact;
			updatedContact.ContactId = originalContactId;

			// Act
			ContactService.Update(updatedContact);
			Models.Contact? actualContact = ContactService.Retrieve(originalContactId);
			
			// Assert
			Assert.AreEqual(updatedContact, actualContact, "Unexpected data");

			// Cleanup
			ContactService.Delete(originalContactId);
		}

		[TestMethod]
		public void Delete()
		{
			// Arrange
			Models.Contact expectedContact = TestData.Contact;
			int expectedContactId = ContactService.Create(expectedContact);

			// Act
			ContactService.Delete(expectedContactId);
			Models.Contact? actualContact = ContactService.Retrieve(expectedContactId);

			// Assert
			Assert.IsNull(actualContact, "Unexpected data");
		}
	}

	internal static class TestData
	{
		public static Models.Contact Contact
		{
			get
			{
				return new Models.Contact {
					                          Address1 = Text.RandomString(50),
					                          Address2 = Text.RandomString(50),
					                          Address3 = Text.RandomString(50),
					                          Address4 = Text.RandomString(50),
					                          Company = Text.RandomString(50),
					                          ContactTypeId = ContactTypeService.RetrieveAll.First().ContactTypeId,
					                          CountryId = CountryService.RetrieveAll.First().CountryId,
					                          EmailAddress = Text.RandomString(50),
					                          Forename = Text.RandomString(50),
					                          Position = Text.RandomString(50),
					                          Postcode = Text.RandomString(50),
					                          Surname = Text.RandomString(50),
					                          Title = Text.RandomString(50),
					                          Website = Text.RandomString(50)
				                          };
			}
		}
	}
}