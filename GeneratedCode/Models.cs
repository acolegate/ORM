using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace GeneratedCode
{
   [ExcludeFromCodeCoverage]
   [DebuggerStepThrough]
   public static class Models
   {
      public enum CalendarEventTypeEnum
      {
         ClubPoolOnlyEvening = 3, 
         ClubPresentationEvening = 11, 
         ClubTheoryAndPoolEvening = 1, 
         ClubTheoryOnlyEvening = 2, 
         CommitteeMeeting = 4, 
         NotSpecified = 0, 
         OffSitesDc = 9, 
         OffSiteTrainingSdcIfcEtc = 10, 
         PleasuredIvE = 6, 
         SocialEvent = 8, 
         TrainingAndPleasuredIvE = 7, 
         TrainingDive = 5, 
      }

      public enum FeedTypeEnum
      {
         RSs = 1, 
      }

      public enum SignOffTypeEnum
      {
         DiverGradeCompleteSignedOff = 9, 
         DryPractical = 5, 
         ExperiencedIvE = 6, 
         MinimumNumberOfDivestIMe = 7, 
         OpenWater = 4, 
         PostQualification = 10, 
         QualifyingDive = 8, 
         SelfDeclaration = 1, 
         ShelteredWater = 3, 
         Theory = 2, 
         Unknown = 0, 
      }

      public struct Article
      {
         public bool AlwaysShowOnHomepage { get; set; }
         public int ArticleId { get; set; }
         public DateTime Created { get; set; }
         public int CreatedByMemberId { get; set; }
         public string Details { get; set; }
         public DateTime Modified { get; set; }
         public int ModifiedByMemberId { get; set; }
         public bool NeverShowOnHomepage { get; set; }
         public string Title { get; set; }
      }

      public struct CalendarEvent
      {
         public decimal BalanceAmount { get; set; }
         public CalendarEventTypeEnum CalendarEventType { get; set; }
         public DateTime Created { get; set; }
         public int CreatedByMemberId { get; set; }
         public DateTime DateFrom { get; set; }
         public DateTime DateTo { get; set; }
         public decimal DepositAmount { get; set; }
         public string Description { get; set; }
         public string Details { get; set; }
         public int EventId { get; set; }
         public string Location { get; set; }
         public int MinimumDiverGradeId { get; set; }
         public DateTime Modified { get; set; }
         public int ModifiedByMemberId { get; set; }
         public int MoneyRecipient { get; set; }
         public int OrganisedByMemberId { get; set; }
         public string PrivateNotes { get; set; }
         public int RelatedArticleId { get; set; }
         public string SpacesLeft { get; set; }
      }

      public struct CalendarEventMember
      {
         public int CalendarEventMemberId { get; set; }
         public DateTime Created { get; set; }
         public int EventId { get; set; }
         public int MemberId { get; set; }
         public bool PaidBalance { get; set; }
         public DateTime PaidBalancedAte { get; set; }
         public bool PaidDeposit { get; set; }
         public DateTime PaidDepositDate { get; set; }
      }

      public struct DiverGrade
      {
         public string Abbreviation { get; set; }
         public int DiverGradeId { get; set; }
         public string Name { get; set; }
         public byte Rank { get; set; }
      }

      public struct Feed
      {
         public string Content { get; set; }
         public int FeedCategoryId { get; set; }
         public int FeedId { get; set; }
         public int FeedTypeId { get; set; }
         public DateTime LastRetrieved { get; set; }
         public string Name { get; set; }
         public string Url { get; set; }
      }

      public struct FeedCategory
      {
         public int FeedCategoryId { get; set; }
         public string Name { get; set; }
      }

      public struct Gallery
      {
         public DateTime Created { get; set; }
         public int CreatedByMemberId { get; set; }
         public string Description { get; set; }
         public int GalleryId { get; set; }
         public bool IsTopGallery { get; set; }
         public DateTime Modified { get; set; }
         public int ModifiedByMemberId { get; set; }
         public string Name { get; set; }
      }

      public struct GalleryImage
      {
         public DateTime Created { get; set; }
         public int CreatedByMemberId { get; set; }
         public string Description { get; set; }
         public string FullSizeHashFileName { get; set; }
         public int FullSizeHeight { get; set; }
         public int FullSizeWidth { get; set; }
         public int GalleryId { get; set; }
         public int GalleryImageId { get; set; }
         public byte HashFolder { get; set; }
         public DateTime Modified { get; set; }
         public int ModifiedByMemberId { get; set; }
         public string Name { get; set; }
         public string OriginalFileName { get; set; }
         public string ThumbnailHashFileName { get; set; }
         public int ThumbnailHeight { get; set; }
         public int ThumbnailWidth { get; set; }
         public bool USeasGalleryImage { get; set; }
      }

      public struct KitCategory
      {
         public int KitCategoryId { get; set; }
         public string Name { get; set; }
      }

      public struct KitForSale
      {
         public DateTime Created { get; set; }
         public string Description { get; set; }
         public int KitCategoryId { get; set; }
         public int KitForSaleId { get; set; }
         public decimal MaXPrice { get; set; }
         public int MemberId { get; set; }
         public decimal MinPrice { get; set; }
         public DateTime Modified { get; set; }
         public string Name { get; set; }
      }

      public struct Member
      {
         public string Address1 { get; set; }
         public string Address2 { get; set; }
         public bool Administrator { get; set; }
         public bool AssistantInstructor { get; set; }
         public string BsaCMembershipNumber { get; set; }
         public bool Chairman { get; set; }
         public string County { get; set; }
         public int DiverGradeId { get; set; }
         public bool DivingOfficer { get; set; }
         public string EmailAddress1 { get; set; }
         public string EmailAddress2 { get; set; }
         public string EmergencyContactName { get; set; }
         public string EmergencyContactPhoneNumber { get; set; }
         public string EmergencyContactRelationship { get; set; }
         public bool EquipmentOfficer { get; set; }
         public bool FirstAider { get; set; }
         public string Forename { get; set; }
         public bool HideMember { get; set; }
         public string HomePhoneNumber { get; set; }
         public string Initials { get; set; }
         public bool Instructor { get; set; }
         public bool IsDeleted { get; set; }
         public DateTime LastActivity { get; set; }
         public int MemberId { get; set; }
         public string MobilePhoneNumber { get; set; }
         public string Notes { get; set; }
         public bool O2AdMin { get; set; }
         public string Password { get; set; }
         public string Postcode { get; set; }
         public bool Secretary { get; set; }
         public string SessionGuiD { get; set; }
         public bool SocialSecretary { get; set; }
         public string Surname { get; set; }
         public string Town { get; set; }
         public bool TrainingOfficer { get; set; }
         public bool Treasurer { get; set; }
         public bool UseCommaAsAddressSeparator { get; set; }
         public string UserName { get; set; }
      }

      public struct SignOff
      {
         public byte DisplayOrder { get; set; }
         public int DiverGradeId { get; set; }
         public string ModuleCode { get; set; }
         public string Name { get; set; }
         public int SignOffId { get; set; }
         public SignOffTypeEnum SignOffType { get; set; }
      }

      public struct SignOffRecord
      {
         public int MemberId { get; set; }
         public string Notes { get; set; }
         public DateTime SignOffDate { get; set; }
         public int SignOffId { get; set; }
         public int SignOffRecordId { get; set; }
         public int SignedOffByMemberId { get; set; }
      }

      public struct UploadedFile
      {
         public string ArchiveFileName { get; set; }
         public string ContentType { get; set; }
         public DateTime Created { get; set; }
         public int CreatedByMemberId { get; set; }
         public string Description { get; set; }
         public int ImageHeight { get; set; }
         public int ImageWidth { get; set; }
         public DateTime Modified { get; set; }
         public int ModifiedByMemberId { get; set; }
         public string Name { get; set; }
         public string Notes { get; set; }
         public string OriginalFileName { get; set; }
         public int Size { get; set; }
         public string ThumbnailFileName { get; set; }
         public int UploadedFileId { get; set; }
      }

      public struct Website
      {
         public int SiteHitCount { get; set; }
         public DateTime SiteLastHit { get; set; }
         public int WebsiteId { get; set; }
      }
   }
}
