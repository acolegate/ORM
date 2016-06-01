using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace GeneratedCode
{
   [ExcludeFromCodeCoverage]
   [DebuggerStepThrough]
   public static class ArticleService
   {
      public static List<Models.Article> RetrieveAll
      {
         get
         {
            List<Models.Article> articleList = new List<Models.Article>();

            using (SqlCommand sqlCommand = new SqlCommand("select [articleid],[createdbymemberid],[title],[details],[created],[modified],[modifiedbymemberid],[nevershowonhomepage],[alwaysshowonhomepage] from [dbo].[Article];"))
            {
               Models.Article? article;
               foreach (DataRow dataRow in Commands.RetrieveDataTable(sqlCommand).Rows)
               {
                  article = PopulateArticleModel(dataRow);
                  if (article != null)
                  {
                     articleList.Add((Models.Article)article);
                  }
               }
            }

            return articleList;
         }
      }

      public static int Create(Models.Article article)
      {
         using (SqlCommand sqlCommand = new SqlCommand("insert into [dbo].[Article]([createdbymemberid],[title],[details],[created],[modified],[modifiedbymemberid],[nevershowonhomepage],[alwaysshowonhomepage]) values(@createdbymemberid,@title,@details,@created,@modified,@modifiedbymemberid,@nevershowonhomepage,@alwaysshowonhomepage); select scope_identity();"))
         {
            sqlCommand.Parameters.AddWithValue("@createdbymemberid", article.CreatedByMemberId);
            sqlCommand.Parameters.AddWithValue("@title", article.Title);
            sqlCommand.Parameters.AddWithValue("@details", article.Details);
            sqlCommand.Parameters.AddWithValue("@created", article.Created);
            sqlCommand.Parameters.AddWithValue("@modified", article.Modified);
            sqlCommand.Parameters.AddWithValue("@modifiedbymemberid", article.ModifiedByMemberId);
            sqlCommand.Parameters.AddWithValue("@nevershowonhomepage", article.NeverShowOnHomepage);
            sqlCommand.Parameters.AddWithValue("@alwaysshowonhomepage", article.AlwaysShowOnHomepage);

            return Convert.ToInt32(Commands.RetrieveScalarValue(sqlCommand));
         }
      }

      public static void Delete(int articleId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("delete [dbo].[Article] where [articleid]=@articleid;"))
         {
            sqlCommand.Parameters.AddWithValue("@articleid", articleId);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      public static Models.Article? Retrieve(int articleId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("select [articleid],[createdbymemberid],[title],[details],[created],[modified],[modifiedbymemberid],[nevershowonhomepage],[alwaysshowonhomepage] from [dbo].[Article] where [articleid]=@articleid;"))
         {
            sqlCommand.Parameters.AddWithValue("@articleid", articleId);

            return PopulateArticleModel(Commands.RetrieveDataRow(sqlCommand));
         }
      }

      public static void Update(Models.Article article)
      {
         using (SqlCommand sqlCommand = new SqlCommand("update [dbo].[Article] set [createdbymemberid]=@createdbymemberid,[title]=@title,[details]=@details,[created]=@created,[modified]=@modified,[modifiedbymemberid]=@modifiedbymemberid,[nevershowonhomepage]=@nevershowonhomepage,[alwaysshowonhomepage]=@alwaysshowonhomepage where [articleid]=@articleid;"))
         {
            sqlCommand.Parameters.AddWithValue("@articleid", article.ArticleId);
            sqlCommand.Parameters.AddWithValue("@createdbymemberid", article.CreatedByMemberId);
            sqlCommand.Parameters.AddWithValue("@title", article.Title);
            sqlCommand.Parameters.AddWithValue("@details", article.Details);
            sqlCommand.Parameters.AddWithValue("@created", article.Created);
            sqlCommand.Parameters.AddWithValue("@modified", article.Modified);
            sqlCommand.Parameters.AddWithValue("@modifiedbymemberid", article.ModifiedByMemberId);
            sqlCommand.Parameters.AddWithValue("@nevershowonhomepage", article.NeverShowOnHomepage);
            sqlCommand.Parameters.AddWithValue("@alwaysshowonhomepage", article.AlwaysShowOnHomepage);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      private static Models.Article? PopulateArticleModel(DataRow dataRow)
      {
         if (dataRow != null)
         {
            return new Models.Article {
                                         ArticleId = Convert.ToInt32(dataRow["articleid"]), 
                                         CreatedByMemberId = Convert.ToInt32(dataRow["createdbymemberid"]), 
                                         Title = Convert.ToString(dataRow["title"]), 
                                         Details = Convert.ToString(dataRow["details"]), 
                                         Created = Convert.ToDateTime(dataRow["created"]), 
                                         Modified = Convert.ToDateTime(dataRow["modified"]), 
                                         ModifiedByMemberId = Convert.ToInt32(dataRow["modifiedbymemberid"]), 
                                         NeverShowOnHomepage = Convert.ToBoolean(dataRow["nevershowonhomepage"]), 
                                         AlwaysShowOnHomepage = Convert.ToBoolean(dataRow["alwaysshowonhomepage"]), 
                                      };
         }

         return null;
      }
   }

   [ExcludeFromCodeCoverage]
   [DebuggerStepThrough]
   public static class CalendarEventService
   {
      public static List<Models.CalendarEvent> RetrieveAll
      {
         get
         {
            List<Models.CalendarEvent> calendarEventList = new List<Models.CalendarEvent>();

            using (SqlCommand sqlCommand = new SqlCommand("select [eventid],[CalendarEventTypeId],[datefrom],[dateto],[description],[location],[organisedbymemberid],[minimumdivergradeid],[spacesleft],[details],[privatenotes],[relatedarticleid],[depositamount],[balanceamount],[moneyrecipient],[createdbymemberid],[modifiedbymemberid],[modified],[created] from [dbo].[CalendarEvent];"))
            {
               Models.CalendarEvent? calendarEvent;
               foreach (DataRow dataRow in Commands.RetrieveDataTable(sqlCommand).Rows)
               {
                  calendarEvent = PopulateCalendarEventModel(dataRow);
                  if (calendarEvent != null)
                  {
                     calendarEventList.Add((Models.CalendarEvent)calendarEvent);
                  }
               }
            }

            return calendarEventList;
         }
      }

      public static int Create(Models.CalendarEvent calendarEvent)
      {
         using (SqlCommand sqlCommand = new SqlCommand("insert into [dbo].[CalendarEvent]([CalendarEventTypeId],[datefrom],[dateto],[description],[location],[organisedbymemberid],[minimumdivergradeid],[spacesleft],[details],[privatenotes],[relatedarticleid],[depositamount],[balanceamount],[moneyrecipient],[createdbymemberid],[modifiedbymemberid],[modified],[created]) values(@CalendarEventTypeId,@datefrom,@dateto,@description,@location,@organisedbymemberid,@minimumdivergradeid,@spacesleft,@details,@privatenotes,@relatedarticleid,@depositamount,@balanceamount,@moneyrecipient,@createdbymemberid,@modifiedbymemberid,@modified,@created); select scope_identity();"))
         {
            sqlCommand.Parameters.AddWithValue("@CalendarEventTypeId", calendarEvent.CalendarEventType);
            sqlCommand.Parameters.AddWithValue("@datefrom", calendarEvent.DateFrom);
            sqlCommand.Parameters.AddWithValue("@dateto", calendarEvent.DateTo);
            sqlCommand.Parameters.AddWithValue("@description", calendarEvent.Description);
            sqlCommand.Parameters.AddWithValue("@location", calendarEvent.Location);
            sqlCommand.Parameters.AddWithValue("@organisedbymemberid", calendarEvent.OrganisedByMemberId);
            sqlCommand.Parameters.AddWithValue("@minimumdivergradeid", calendarEvent.MinimumDiverGradeId);
            sqlCommand.Parameters.AddWithValue("@spacesleft", calendarEvent.SpacesLeft);
            sqlCommand.Parameters.AddWithValue("@details", calendarEvent.Details);
            sqlCommand.Parameters.AddWithValue("@privatenotes", calendarEvent.PrivateNotes);
            sqlCommand.Parameters.AddWithValue("@relatedarticleid", calendarEvent.RelatedArticleId);
            sqlCommand.Parameters.AddWithValue("@depositamount", calendarEvent.DepositAmount);
            sqlCommand.Parameters.AddWithValue("@balanceamount", calendarEvent.BalanceAmount);
            sqlCommand.Parameters.AddWithValue("@moneyrecipient", calendarEvent.MoneyRecipient);
            sqlCommand.Parameters.AddWithValue("@createdbymemberid", calendarEvent.CreatedByMemberId);
            sqlCommand.Parameters.AddWithValue("@modifiedbymemberid", calendarEvent.ModifiedByMemberId);
            sqlCommand.Parameters.AddWithValue("@modified", calendarEvent.Modified);
            sqlCommand.Parameters.AddWithValue("@created", calendarEvent.Created);

            return Convert.ToInt32(Commands.RetrieveScalarValue(sqlCommand));
         }
      }

      public static void Delete(int eventId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("delete [dbo].[CalendarEvent] where [eventid]=@eventid;"))
         {
            sqlCommand.Parameters.AddWithValue("@eventid", eventId);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      public static Models.CalendarEvent? Retrieve(int eventId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("select [eventid],[CalendarEventTypeId],[datefrom],[dateto],[description],[location],[organisedbymemberid],[minimumdivergradeid],[spacesleft],[details],[privatenotes],[relatedarticleid],[depositamount],[balanceamount],[moneyrecipient],[createdbymemberid],[modifiedbymemberid],[modified],[created] from [dbo].[CalendarEvent] where [eventid]=@eventid;"))
         {
            sqlCommand.Parameters.AddWithValue("@eventid", eventId);

            return PopulateCalendarEventModel(Commands.RetrieveDataRow(sqlCommand));
         }
      }

      public static void Update(Models.CalendarEvent calendarEvent)
      {
         using (SqlCommand sqlCommand = new SqlCommand("update [dbo].[CalendarEvent] set [CalendarEventTypeId]=@CalendarEventTypeId,[datefrom]=@datefrom,[dateto]=@dateto,[description]=@description,[location]=@location,[organisedbymemberid]=@organisedbymemberid,[minimumdivergradeid]=@minimumdivergradeid,[spacesleft]=@spacesleft,[details]=@details,[privatenotes]=@privatenotes,[relatedarticleid]=@relatedarticleid,[depositamount]=@depositamount,[balanceamount]=@balanceamount,[moneyrecipient]=@moneyrecipient,[createdbymemberid]=@createdbymemberid,[modifiedbymemberid]=@modifiedbymemberid,[modified]=@modified,[created]=@created where [eventid]=@eventid;"))
         {
            sqlCommand.Parameters.AddWithValue("@eventid", calendarEvent.EventId);
            sqlCommand.Parameters.AddWithValue("@CalendarEventTypeId", calendarEvent.CalendarEventType);
            sqlCommand.Parameters.AddWithValue("@datefrom", calendarEvent.DateFrom);
            sqlCommand.Parameters.AddWithValue("@dateto", calendarEvent.DateTo);
            sqlCommand.Parameters.AddWithValue("@description", calendarEvent.Description);
            sqlCommand.Parameters.AddWithValue("@location", calendarEvent.Location);
            sqlCommand.Parameters.AddWithValue("@organisedbymemberid", calendarEvent.OrganisedByMemberId);
            sqlCommand.Parameters.AddWithValue("@minimumdivergradeid", calendarEvent.MinimumDiverGradeId);
            sqlCommand.Parameters.AddWithValue("@spacesleft", calendarEvent.SpacesLeft);
            sqlCommand.Parameters.AddWithValue("@details", calendarEvent.Details);
            sqlCommand.Parameters.AddWithValue("@privatenotes", calendarEvent.PrivateNotes);
            sqlCommand.Parameters.AddWithValue("@relatedarticleid", calendarEvent.RelatedArticleId);
            sqlCommand.Parameters.AddWithValue("@depositamount", calendarEvent.DepositAmount);
            sqlCommand.Parameters.AddWithValue("@balanceamount", calendarEvent.BalanceAmount);
            sqlCommand.Parameters.AddWithValue("@moneyrecipient", calendarEvent.MoneyRecipient);
            sqlCommand.Parameters.AddWithValue("@createdbymemberid", calendarEvent.CreatedByMemberId);
            sqlCommand.Parameters.AddWithValue("@modifiedbymemberid", calendarEvent.ModifiedByMemberId);
            sqlCommand.Parameters.AddWithValue("@modified", calendarEvent.Modified);
            sqlCommand.Parameters.AddWithValue("@created", calendarEvent.Created);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      private static Models.CalendarEvent? PopulateCalendarEventModel(DataRow dataRow)
      {
         if (dataRow != null)
         {
            return new Models.CalendarEvent {
                                               EventId = Convert.ToInt32(dataRow["eventid"]), 
                                               CalendarEventType = (Models.CalendarEventTypeEnum)Convert.ToInt32(dataRow["CalendarEventTypeId"]), 
                                               DateFrom = Convert.ToDateTime(dataRow["datefrom"]), 
                                               DateTo = Convert.ToDateTime(dataRow["dateto"]), 
                                               Description = Convert.ToString(dataRow["description"]), 
                                               Location = Convert.ToString(dataRow["location"]), 
                                               OrganisedByMemberId = Convert.ToInt32(dataRow["organisedbymemberid"]), 
                                               MinimumDiverGradeId = Convert.ToInt32(dataRow["minimumdivergradeid"]), 
                                               SpacesLeft = Convert.ToString(dataRow["spacesleft"]), 
                                               Details = Convert.ToString(dataRow["details"]), 
                                               PrivateNotes = Convert.ToString(dataRow["privatenotes"]), 
                                               RelatedArticleId = Convert.ToInt32(dataRow["relatedarticleid"]), 
                                               DepositAmount = Convert.ToDecimal(dataRow["depositamount"]), 
                                               BalanceAmount = Convert.ToDecimal(dataRow["balanceamount"]), 
                                               MoneyRecipient = Convert.ToInt32(dataRow["moneyrecipient"]), 
                                               CreatedByMemberId = Convert.ToInt32(dataRow["createdbymemberid"]), 
                                               ModifiedByMemberId = Convert.ToInt32(dataRow["modifiedbymemberid"]), 
                                               Modified = Convert.ToDateTime(dataRow["modified"]), 
                                               Created = Convert.ToDateTime(dataRow["created"]), 
                                            };
         }

         return null;
      }
   }

   [ExcludeFromCodeCoverage]
   [DebuggerStepThrough]
   public static class CalendarEventMemberService
   {
      public static List<Models.CalendarEventMember> RetrieveAll
      {
         get
         {
            List<Models.CalendarEventMember> calendarEventMemberList = new List<Models.CalendarEventMember>();

            using (SqlCommand sqlCommand = new SqlCommand("select [calendareventmemberid],[eventid],[memberid],[paiddeposit],[paiddepositdate],[paidbalance],[paidbalancedate],[created] from [dbo].[CalendarEventMember];"))
            {
               Models.CalendarEventMember? calendarEventMember;
               foreach (DataRow dataRow in Commands.RetrieveDataTable(sqlCommand).Rows)
               {
                  calendarEventMember = PopulateCalendarEventMemberModel(dataRow);
                  if (calendarEventMember != null)
                  {
                     calendarEventMemberList.Add((Models.CalendarEventMember)calendarEventMember);
                  }
               }
            }

            return calendarEventMemberList;
         }
      }

      public static int Create(Models.CalendarEventMember calendarEventMember)
      {
         using (SqlCommand sqlCommand = new SqlCommand("insert into [dbo].[CalendarEventMember]([eventid],[memberid],[paiddeposit],[paiddepositdate],[paidbalance],[paidbalancedate],[created]) values(@eventid,@memberid,@paiddeposit,@paiddepositdate,@paidbalance,@paidbalancedate,@created); select scope_identity();"))
         {
            sqlCommand.Parameters.AddWithValue("@eventid", calendarEventMember.EventId);
            sqlCommand.Parameters.AddWithValue("@memberid", calendarEventMember.MemberId);
            sqlCommand.Parameters.AddWithValue("@paiddeposit", calendarEventMember.PaidDeposit);
            sqlCommand.Parameters.AddWithValue("@paiddepositdate", calendarEventMember.PaidDepositDate);
            sqlCommand.Parameters.AddWithValue("@paidbalance", calendarEventMember.PaidBalance);
            sqlCommand.Parameters.AddWithValue("@paidbalancedate", calendarEventMember.PaidBalancedAte);
            sqlCommand.Parameters.AddWithValue("@created", calendarEventMember.Created);

            return Convert.ToInt32(Commands.RetrieveScalarValue(sqlCommand));
         }
      }

      public static void Delete(int calendarEventMemberId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("delete [dbo].[CalendarEventMember] where [calendareventmemberid]=@calendareventmemberid;"))
         {
            sqlCommand.Parameters.AddWithValue("@calendareventmemberid", calendarEventMemberId);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      public static Models.CalendarEventMember? Retrieve(int calendarEventMemberId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("select [calendareventmemberid],[eventid],[memberid],[paiddeposit],[paiddepositdate],[paidbalance],[paidbalancedate],[created] from [dbo].[CalendarEventMember] where [calendareventmemberid]=@calendareventmemberid;"))
         {
            sqlCommand.Parameters.AddWithValue("@calendareventmemberid", calendarEventMemberId);

            return PopulateCalendarEventMemberModel(Commands.RetrieveDataRow(sqlCommand));
         }
      }

      public static void Update(Models.CalendarEventMember calendarEventMember)
      {
         using (SqlCommand sqlCommand = new SqlCommand("update [dbo].[CalendarEventMember] set [eventid]=@eventid,[memberid]=@memberid,[paiddeposit]=@paiddeposit,[paiddepositdate]=@paiddepositdate,[paidbalance]=@paidbalance,[paidbalancedate]=@paidbalancedate,[created]=@created where [calendareventmemberid]=@calendareventmemberid;"))
         {
            sqlCommand.Parameters.AddWithValue("@calendareventmemberid", calendarEventMember.CalendarEventMemberId);
            sqlCommand.Parameters.AddWithValue("@eventid", calendarEventMember.EventId);
            sqlCommand.Parameters.AddWithValue("@memberid", calendarEventMember.MemberId);
            sqlCommand.Parameters.AddWithValue("@paiddeposit", calendarEventMember.PaidDeposit);
            sqlCommand.Parameters.AddWithValue("@paiddepositdate", calendarEventMember.PaidDepositDate);
            sqlCommand.Parameters.AddWithValue("@paidbalance", calendarEventMember.PaidBalance);
            sqlCommand.Parameters.AddWithValue("@paidbalancedate", calendarEventMember.PaidBalancedAte);
            sqlCommand.Parameters.AddWithValue("@created", calendarEventMember.Created);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      private static Models.CalendarEventMember? PopulateCalendarEventMemberModel(DataRow dataRow)
      {
         if (dataRow != null)
         {
            return new Models.CalendarEventMember {
                                                     CalendarEventMemberId = Convert.ToInt32(dataRow["calendareventmemberid"]), 
                                                     EventId = Convert.ToInt32(dataRow["eventid"]), 
                                                     MemberId = Convert.ToInt32(dataRow["memberid"]), 
                                                     PaidDeposit = Convert.ToBoolean(dataRow["paiddeposit"]), 
                                                     PaidDepositDate = Convert.ToDateTime(dataRow["paiddepositdate"]), 
                                                     PaidBalance = Convert.ToBoolean(dataRow["paidbalance"]), 
                                                     PaidBalancedAte = Convert.ToDateTime(dataRow["paidbalancedate"]), 
                                                     Created = Convert.ToDateTime(dataRow["created"]), 
                                                  };
         }

         return null;
      }
   }

   [ExcludeFromCodeCoverage]
   [DebuggerStepThrough]
   public static class DiverGradeService
   {
      public static List<Models.DiverGrade> RetrieveAll
      {
         get
         {
            List<Models.DiverGrade> diverGradeList = new List<Models.DiverGrade>();

            using (SqlCommand sqlCommand = new SqlCommand("select [divergradeid],[name],[abbreviation],[rank] from [dbo].[DiverGrade];"))
            {
               Models.DiverGrade? diverGrade;
               foreach (DataRow dataRow in Commands.RetrieveDataTable(sqlCommand).Rows)
               {
                  diverGrade = PopulateDiverGradeModel(dataRow);
                  if (diverGrade != null)
                  {
                     diverGradeList.Add((Models.DiverGrade)diverGrade);
                  }
               }
            }

            return diverGradeList;
         }
      }

      public static int Create(Models.DiverGrade diverGrade)
      {
         using (SqlCommand sqlCommand = new SqlCommand("insert into [dbo].[DiverGrade]([name],[abbreviation],[rank]) values(@name,@abbreviation,@rank); select scope_identity();"))
         {
            sqlCommand.Parameters.AddWithValue("@name", diverGrade.Name);
            sqlCommand.Parameters.AddWithValue("@abbreviation", diverGrade.Abbreviation);
            sqlCommand.Parameters.AddWithValue("@rank", diverGrade.Rank);

            return Convert.ToInt32(Commands.RetrieveScalarValue(sqlCommand));
         }
      }

      public static void Delete(int diverGradeId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("delete [dbo].[DiverGrade] where [divergradeid]=@divergradeid;"))
         {
            sqlCommand.Parameters.AddWithValue("@divergradeid", diverGradeId);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      public static Models.DiverGrade? Retrieve(int diverGradeId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("select [divergradeid],[name],[abbreviation],[rank] from [dbo].[DiverGrade] where [divergradeid]=@divergradeid;"))
         {
            sqlCommand.Parameters.AddWithValue("@divergradeid", diverGradeId);

            return PopulateDiverGradeModel(Commands.RetrieveDataRow(sqlCommand));
         }
      }

      public static void Update(Models.DiverGrade diverGrade)
      {
         using (SqlCommand sqlCommand = new SqlCommand("update [dbo].[DiverGrade] set [name]=@name,[abbreviation]=@abbreviation,[rank]=@rank where [divergradeid]=@divergradeid;"))
         {
            sqlCommand.Parameters.AddWithValue("@divergradeid", diverGrade.DiverGradeId);
            sqlCommand.Parameters.AddWithValue("@name", diverGrade.Name);
            sqlCommand.Parameters.AddWithValue("@abbreviation", diverGrade.Abbreviation);
            sqlCommand.Parameters.AddWithValue("@rank", diverGrade.Rank);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      private static Models.DiverGrade? PopulateDiverGradeModel(DataRow dataRow)
      {
         if (dataRow != null)
         {
            return new Models.DiverGrade {
                                            DiverGradeId = Convert.ToInt32(dataRow["divergradeid"]), 
                                            Name = Convert.ToString(dataRow["name"]), 
                                            Abbreviation = Convert.ToString(dataRow["abbreviation"]), 
                                            Rank = Convert.ToByte(dataRow["rank"]), 
                                         };
         }

         return null;
      }
   }

   [ExcludeFromCodeCoverage]
   [DebuggerStepThrough]
   public static class FeedService
   {
      public static List<Models.Feed> RetrieveAll
      {
         get
         {
            List<Models.Feed> feedList = new List<Models.Feed>();

            using (SqlCommand sqlCommand = new SqlCommand("select [feedid],[name],[url],[feedtypeid],[feedcategoryid],[lastretrieved],[content] from [dbo].[Feed];"))
            {
               Models.Feed? feed;
               foreach (DataRow dataRow in Commands.RetrieveDataTable(sqlCommand).Rows)
               {
                  feed = PopulateFeedModel(dataRow);
                  if (feed != null)
                  {
                     feedList.Add((Models.Feed)feed);
                  }
               }
            }

            return feedList;
         }
      }

      public static int Create(Models.Feed feed)
      {
         using (SqlCommand sqlCommand = new SqlCommand("insert into [dbo].[Feed]([name],[url],[feedtypeid],[feedcategoryid],[lastretrieved],[content]) values(@name,@url,@feedtypeid,@feedcategoryid,@lastretrieved,@content); select scope_identity();"))
         {
            sqlCommand.Parameters.AddWithValue("@name", feed.Name);
            sqlCommand.Parameters.AddWithValue("@url", feed.Url);
            sqlCommand.Parameters.AddWithValue("@feedtypeid", feed.FeedTypeId);
            sqlCommand.Parameters.AddWithValue("@feedcategoryid", feed.FeedCategoryId);
            sqlCommand.Parameters.AddWithValue("@lastretrieved", feed.LastRetrieved);
            sqlCommand.Parameters.AddWithValue("@content", feed.Content);

            return Convert.ToInt32(Commands.RetrieveScalarValue(sqlCommand));
         }
      }

      public static void Delete(int feedId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("delete [dbo].[Feed] where [feedid]=@feedid;"))
         {
            sqlCommand.Parameters.AddWithValue("@feedid", feedId);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      public static Models.Feed? Retrieve(int feedId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("select [feedid],[name],[url],[feedtypeid],[feedcategoryid],[lastretrieved],[content] from [dbo].[Feed] where [feedid]=@feedid;"))
         {
            sqlCommand.Parameters.AddWithValue("@feedid", feedId);

            return PopulateFeedModel(Commands.RetrieveDataRow(sqlCommand));
         }
      }

      public static void Update(Models.Feed feed)
      {
         using (SqlCommand sqlCommand = new SqlCommand("update [dbo].[Feed] set [name]=@name,[url]=@url,[feedtypeid]=@feedtypeid,[feedcategoryid]=@feedcategoryid,[lastretrieved]=@lastretrieved,[content]=@content where [feedid]=@feedid;"))
         {
            sqlCommand.Parameters.AddWithValue("@feedid", feed.FeedId);
            sqlCommand.Parameters.AddWithValue("@name", feed.Name);
            sqlCommand.Parameters.AddWithValue("@url", feed.Url);
            sqlCommand.Parameters.AddWithValue("@feedtypeid", feed.FeedTypeId);
            sqlCommand.Parameters.AddWithValue("@feedcategoryid", feed.FeedCategoryId);
            sqlCommand.Parameters.AddWithValue("@lastretrieved", feed.LastRetrieved);
            sqlCommand.Parameters.AddWithValue("@content", feed.Content);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      private static Models.Feed? PopulateFeedModel(DataRow dataRow)
      {
         if (dataRow != null)
         {
            return new Models.Feed {
                                      FeedId = Convert.ToInt32(dataRow["feedid"]), 
                                      Name = Convert.ToString(dataRow["name"]), 
                                      Url = Convert.ToString(dataRow["url"]), 
                                      FeedTypeId = Convert.ToInt32(dataRow["feedtypeid"]), 
                                      FeedCategoryId = Convert.ToInt32(dataRow["feedcategoryid"]), 
                                      LastRetrieved = Convert.ToDateTime(dataRow["lastretrieved"]), 
                                      Content = Convert.ToString(dataRow["content"]), 
                                   };
         }

         return null;
      }
   }

   [ExcludeFromCodeCoverage]
   [DebuggerStepThrough]
   public static class FeedCategoryService
   {
      public static List<Models.FeedCategory> RetrieveAll
      {
         get
         {
            List<Models.FeedCategory> feedCategoryList = new List<Models.FeedCategory>();

            using (SqlCommand sqlCommand = new SqlCommand("select [feedcategoryid],[name] from [dbo].[FeedCategory];"))
            {
               Models.FeedCategory? feedCategory;
               foreach (DataRow dataRow in Commands.RetrieveDataTable(sqlCommand).Rows)
               {
                  feedCategory = PopulateFeedCategoryModel(dataRow);
                  if (feedCategory != null)
                  {
                     feedCategoryList.Add((Models.FeedCategory)feedCategory);
                  }
               }
            }

            return feedCategoryList;
         }
      }

      public static int Create(Models.FeedCategory feedCategory)
      {
         using (SqlCommand sqlCommand = new SqlCommand("insert into [dbo].[FeedCategory]([feedcategoryid],[name]) values(@feedcategoryid,@name); select scope_identity();"))
         {
            sqlCommand.Parameters.AddWithValue("@feedcategoryid", feedCategory.FeedCategoryId);
            sqlCommand.Parameters.AddWithValue("@name", feedCategory.Name);

            return Convert.ToInt32(Commands.RetrieveScalarValue(sqlCommand));
         }
      }

      public static void Delete(int feedCategoryId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("delete [dbo].[FeedCategory] where [feedcategoryid]=@feedcategoryid;"))
         {
            sqlCommand.Parameters.AddWithValue("@feedcategoryid", feedCategoryId);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      public static Models.FeedCategory? Retrieve(int feedCategoryId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("select [feedcategoryid],[name] from [dbo].[FeedCategory] where [feedcategoryid]=@feedcategoryid;"))
         {
            sqlCommand.Parameters.AddWithValue("@feedcategoryid", feedCategoryId);

            return PopulateFeedCategoryModel(Commands.RetrieveDataRow(sqlCommand));
         }
      }

      public static void Update(Models.FeedCategory feedCategory)
      {
         using (SqlCommand sqlCommand = new SqlCommand("update [dbo].[FeedCategory] set [feedcategoryid]=@feedcategoryid,[name]=@name where [feedcategoryid]=@feedcategoryid;"))
         {
            sqlCommand.Parameters.AddWithValue("@feedcategoryid", feedCategory.FeedCategoryId);
            sqlCommand.Parameters.AddWithValue("@name", feedCategory.Name);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      private static Models.FeedCategory? PopulateFeedCategoryModel(DataRow dataRow)
      {
         if (dataRow != null)
         {
            return new Models.FeedCategory {
                                              FeedCategoryId = Convert.ToInt32(dataRow["feedcategoryid"]), 
                                              Name = Convert.ToString(dataRow["name"]), 
                                           };
         }

         return null;
      }
   }

   [ExcludeFromCodeCoverage]
   [DebuggerStepThrough]
   public static class GalleryService
   {
      public static List<Models.Gallery> RetrieveAll
      {
         get
         {
            List<Models.Gallery> galleryList = new List<Models.Gallery>();

            using (SqlCommand sqlCommand = new SqlCommand("select [GalleryId],[Name],[Description],[IsTopGallery],[CreatedByMemberId],[Created],[ModifiedByMemberId],[Modified] from [dbo].[Gallery];"))
            {
               Models.Gallery? gallery;
               foreach (DataRow dataRow in Commands.RetrieveDataTable(sqlCommand).Rows)
               {
                  gallery = PopulateGalleryModel(dataRow);
                  if (gallery != null)
                  {
                     galleryList.Add((Models.Gallery)gallery);
                  }
               }
            }

            return galleryList;
         }
      }

      public static int Create(Models.Gallery gallery)
      {
         using (SqlCommand sqlCommand = new SqlCommand("insert into [dbo].[Gallery]([Name],[Description],[IsTopGallery],[CreatedByMemberId],[Created],[ModifiedByMemberId],[Modified]) values(@Name,@Description,@IsTopGallery,@CreatedByMemberId,@Created,@ModifiedByMemberId,@Modified); select scope_identity();"))
         {
            sqlCommand.Parameters.AddWithValue("@Name", gallery.Name);
            sqlCommand.Parameters.AddWithValue("@Description", gallery.Description);
            sqlCommand.Parameters.AddWithValue("@IsTopGallery", gallery.IsTopGallery);
            sqlCommand.Parameters.AddWithValue("@CreatedByMemberId", gallery.CreatedByMemberId);
            sqlCommand.Parameters.AddWithValue("@Created", gallery.Created);
            sqlCommand.Parameters.AddWithValue("@ModifiedByMemberId", gallery.ModifiedByMemberId);
            sqlCommand.Parameters.AddWithValue("@Modified", gallery.Modified);

            return Convert.ToInt32(Commands.RetrieveScalarValue(sqlCommand));
         }
      }

      public static void Delete(int galleryId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("delete [dbo].[Gallery] where [GalleryId]=@GalleryId;"))
         {
            sqlCommand.Parameters.AddWithValue("@GalleryId", galleryId);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      public static Models.Gallery? Retrieve(int galleryId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("select [GalleryId],[Name],[Description],[IsTopGallery],[CreatedByMemberId],[Created],[ModifiedByMemberId],[Modified] from [dbo].[Gallery] where [GalleryId]=@GalleryId;"))
         {
            sqlCommand.Parameters.AddWithValue("@GalleryId", galleryId);

            return PopulateGalleryModel(Commands.RetrieveDataRow(sqlCommand));
         }
      }

      public static void Update(Models.Gallery gallery)
      {
         using (SqlCommand sqlCommand = new SqlCommand("update [dbo].[Gallery] set [Name]=@Name,[Description]=@Description,[IsTopGallery]=@IsTopGallery,[CreatedByMemberId]=@CreatedByMemberId,[Created]=@Created,[ModifiedByMemberId]=@ModifiedByMemberId,[Modified]=@Modified where [GalleryId]=@GalleryId;"))
         {
            sqlCommand.Parameters.AddWithValue("@GalleryId", gallery.GalleryId);
            sqlCommand.Parameters.AddWithValue("@Name", gallery.Name);
            sqlCommand.Parameters.AddWithValue("@Description", gallery.Description);
            sqlCommand.Parameters.AddWithValue("@IsTopGallery", gallery.IsTopGallery);
            sqlCommand.Parameters.AddWithValue("@CreatedByMemberId", gallery.CreatedByMemberId);
            sqlCommand.Parameters.AddWithValue("@Created", gallery.Created);
            sqlCommand.Parameters.AddWithValue("@ModifiedByMemberId", gallery.ModifiedByMemberId);
            sqlCommand.Parameters.AddWithValue("@Modified", gallery.Modified);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      private static Models.Gallery? PopulateGalleryModel(DataRow dataRow)
      {
         if (dataRow != null)
         {
            return new Models.Gallery {
                                         GalleryId = Convert.ToInt32(dataRow["GalleryId"]), 
                                         Name = Convert.ToString(dataRow["Name"]), 
                                         Description = Convert.ToString(dataRow["Description"]), 
                                         IsTopGallery = Convert.ToBoolean(dataRow["IsTopGallery"]), 
                                         CreatedByMemberId = Convert.ToInt32(dataRow["CreatedByMemberId"]), 
                                         Created = Convert.ToDateTime(dataRow["Created"]), 
                                         ModifiedByMemberId = Convert.ToInt32(dataRow["ModifiedByMemberId"]), 
                                         Modified = Convert.ToDateTime(dataRow["Modified"]), 
                                      };
         }

         return null;
      }
   }

   [ExcludeFromCodeCoverage]
   [DebuggerStepThrough]
   public static class GalleryImageService
   {
      public static List<Models.GalleryImage> RetrieveAll
      {
         get
         {
            List<Models.GalleryImage> galleryImageList = new List<Models.GalleryImage>();

            using (SqlCommand sqlCommand = new SqlCommand("select [GalleryImageId],[GalleryId],[Name],[Description],[UseAsGalleryImage],[OriginalFilename],[HashFolder],[ThumbnailHashFilename],[FullSizeHashFilename],[FullSizeWidth],[FullSizeHeight],[ThumbnailWidth],[ThumbnailHeight],[CreatedByMemberId],[Created],[ModifiedByMemberId],[Modified] from [dbo].[GalleryImage];"))
            {
               Models.GalleryImage? galleryImage;
               foreach (DataRow dataRow in Commands.RetrieveDataTable(sqlCommand).Rows)
               {
                  galleryImage = PopulateGalleryImageModel(dataRow);
                  if (galleryImage != null)
                  {
                     galleryImageList.Add((Models.GalleryImage)galleryImage);
                  }
               }
            }

            return galleryImageList;
         }
      }

      public static int Create(Models.GalleryImage galleryImage)
      {
         using (SqlCommand sqlCommand = new SqlCommand("insert into [dbo].[GalleryImage]([GalleryId],[Name],[Description],[UseAsGalleryImage],[OriginalFilename],[HashFolder],[ThumbnailHashFilename],[FullSizeHashFilename],[FullSizeWidth],[FullSizeHeight],[ThumbnailWidth],[ThumbnailHeight],[CreatedByMemberId],[Created],[ModifiedByMemberId],[Modified]) values(@GalleryId,@Name,@Description,@UseAsGalleryImage,@OriginalFilename,@HashFolder,@ThumbnailHashFilename,@FullSizeHashFilename,@FullSizeWidth,@FullSizeHeight,@ThumbnailWidth,@ThumbnailHeight,@CreatedByMemberId,@Created,@ModifiedByMemberId,@Modified); select scope_identity();"))
         {
            sqlCommand.Parameters.AddWithValue("@GalleryId", galleryImage.GalleryId);
            sqlCommand.Parameters.AddWithValue("@Name", galleryImage.Name);
            sqlCommand.Parameters.AddWithValue("@Description", galleryImage.Description);
            sqlCommand.Parameters.AddWithValue("@UseAsGalleryImage", galleryImage.USeasGalleryImage);
            sqlCommand.Parameters.AddWithValue("@OriginalFilename", galleryImage.OriginalFileName);
            sqlCommand.Parameters.AddWithValue("@HashFolder", galleryImage.HashFolder);
            sqlCommand.Parameters.AddWithValue("@ThumbnailHashFilename", galleryImage.ThumbnailHashFileName);
            sqlCommand.Parameters.AddWithValue("@FullSizeHashFilename", galleryImage.FullSizeHashFileName);
            sqlCommand.Parameters.AddWithValue("@FullSizeWidth", galleryImage.FullSizeWidth);
            sqlCommand.Parameters.AddWithValue("@FullSizeHeight", galleryImage.FullSizeHeight);
            sqlCommand.Parameters.AddWithValue("@ThumbnailWidth", galleryImage.ThumbnailWidth);
            sqlCommand.Parameters.AddWithValue("@ThumbnailHeight", galleryImage.ThumbnailHeight);
            sqlCommand.Parameters.AddWithValue("@CreatedByMemberId", galleryImage.CreatedByMemberId);
            sqlCommand.Parameters.AddWithValue("@Created", galleryImage.Created);
            sqlCommand.Parameters.AddWithValue("@ModifiedByMemberId", galleryImage.ModifiedByMemberId);
            sqlCommand.Parameters.AddWithValue("@Modified", galleryImage.Modified);

            return Convert.ToInt32(Commands.RetrieveScalarValue(sqlCommand));
         }
      }

      public static void Delete(int galleryImageId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("delete [dbo].[GalleryImage] where [GalleryImageId]=@GalleryImageId;"))
         {
            sqlCommand.Parameters.AddWithValue("@GalleryImageId", galleryImageId);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      public static Models.GalleryImage? Retrieve(int galleryImageId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("select [GalleryImageId],[GalleryId],[Name],[Description],[UseAsGalleryImage],[OriginalFilename],[HashFolder],[ThumbnailHashFilename],[FullSizeHashFilename],[FullSizeWidth],[FullSizeHeight],[ThumbnailWidth],[ThumbnailHeight],[CreatedByMemberId],[Created],[ModifiedByMemberId],[Modified] from [dbo].[GalleryImage] where [GalleryImageId]=@GalleryImageId;"))
         {
            sqlCommand.Parameters.AddWithValue("@GalleryImageId", galleryImageId);

            return PopulateGalleryImageModel(Commands.RetrieveDataRow(sqlCommand));
         }
      }

      public static void Update(Models.GalleryImage galleryImage)
      {
         using (SqlCommand sqlCommand = new SqlCommand("update [dbo].[GalleryImage] set [GalleryId]=@GalleryId,[Name]=@Name,[Description]=@Description,[UseAsGalleryImage]=@UseAsGalleryImage,[OriginalFilename]=@OriginalFilename,[HashFolder]=@HashFolder,[ThumbnailHashFilename]=@ThumbnailHashFilename,[FullSizeHashFilename]=@FullSizeHashFilename,[FullSizeWidth]=@FullSizeWidth,[FullSizeHeight]=@FullSizeHeight,[ThumbnailWidth]=@ThumbnailWidth,[ThumbnailHeight]=@ThumbnailHeight,[CreatedByMemberId]=@CreatedByMemberId,[Created]=@Created,[ModifiedByMemberId]=@ModifiedByMemberId,[Modified]=@Modified where [GalleryImageId]=@GalleryImageId;"))
         {
            sqlCommand.Parameters.AddWithValue("@GalleryImageId", galleryImage.GalleryImageId);
            sqlCommand.Parameters.AddWithValue("@GalleryId", galleryImage.GalleryId);
            sqlCommand.Parameters.AddWithValue("@Name", galleryImage.Name);
            sqlCommand.Parameters.AddWithValue("@Description", galleryImage.Description);
            sqlCommand.Parameters.AddWithValue("@UseAsGalleryImage", galleryImage.USeasGalleryImage);
            sqlCommand.Parameters.AddWithValue("@OriginalFilename", galleryImage.OriginalFileName);
            sqlCommand.Parameters.AddWithValue("@HashFolder", galleryImage.HashFolder);
            sqlCommand.Parameters.AddWithValue("@ThumbnailHashFilename", galleryImage.ThumbnailHashFileName);
            sqlCommand.Parameters.AddWithValue("@FullSizeHashFilename", galleryImage.FullSizeHashFileName);
            sqlCommand.Parameters.AddWithValue("@FullSizeWidth", galleryImage.FullSizeWidth);
            sqlCommand.Parameters.AddWithValue("@FullSizeHeight", galleryImage.FullSizeHeight);
            sqlCommand.Parameters.AddWithValue("@ThumbnailWidth", galleryImage.ThumbnailWidth);
            sqlCommand.Parameters.AddWithValue("@ThumbnailHeight", galleryImage.ThumbnailHeight);
            sqlCommand.Parameters.AddWithValue("@CreatedByMemberId", galleryImage.CreatedByMemberId);
            sqlCommand.Parameters.AddWithValue("@Created", galleryImage.Created);
            sqlCommand.Parameters.AddWithValue("@ModifiedByMemberId", galleryImage.ModifiedByMemberId);
            sqlCommand.Parameters.AddWithValue("@Modified", galleryImage.Modified);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      private static Models.GalleryImage? PopulateGalleryImageModel(DataRow dataRow)
      {
         if (dataRow != null)
         {
            return new Models.GalleryImage {
                                              GalleryImageId = Convert.ToInt32(dataRow["GalleryImageId"]), 
                                              GalleryId = Convert.ToInt32(dataRow["GalleryId"]), 
                                              Name = Convert.ToString(dataRow["Name"]), 
                                              Description = Convert.ToString(dataRow["Description"]), 
                                              USeasGalleryImage = Convert.ToBoolean(dataRow["UseAsGalleryImage"]), 
                                              OriginalFileName = Convert.ToString(dataRow["OriginalFilename"]), 
                                              HashFolder = Convert.ToByte(dataRow["HashFolder"]), 
                                              ThumbnailHashFileName = Convert.ToString(dataRow["ThumbnailHashFilename"]), 
                                              FullSizeHashFileName = Convert.ToString(dataRow["FullSizeHashFilename"]), 
                                              FullSizeWidth = Convert.ToInt32(dataRow["FullSizeWidth"]), 
                                              FullSizeHeight = Convert.ToInt32(dataRow["FullSizeHeight"]), 
                                              ThumbnailWidth = Convert.ToInt32(dataRow["ThumbnailWidth"]), 
                                              ThumbnailHeight = Convert.ToInt32(dataRow["ThumbnailHeight"]), 
                                              CreatedByMemberId = Convert.ToInt32(dataRow["CreatedByMemberId"]), 
                                              Created = Convert.ToDateTime(dataRow["Created"]), 
                                              ModifiedByMemberId = Convert.ToInt32(dataRow["ModifiedByMemberId"]), 
                                              Modified = Convert.ToDateTime(dataRow["Modified"]), 
                                           };
         }

         return null;
      }
   }

   [ExcludeFromCodeCoverage]
   [DebuggerStepThrough]
   public static class KitCategoryService
   {
      public static List<Models.KitCategory> RetrieveAll
      {
         get
         {
            List<Models.KitCategory> kitCategoryList = new List<Models.KitCategory>();

            using (SqlCommand sqlCommand = new SqlCommand("select [kitcategoryid],[name] from [dbo].[KitCategory];"))
            {
               Models.KitCategory? kitCategory;
               foreach (DataRow dataRow in Commands.RetrieveDataTable(sqlCommand).Rows)
               {
                  kitCategory = PopulateKitCategoryModel(dataRow);
                  if (kitCategory != null)
                  {
                     kitCategoryList.Add((Models.KitCategory)kitCategory);
                  }
               }
            }

            return kitCategoryList;
         }
      }

      public static int Create(Models.KitCategory kitCategory)
      {
         using (SqlCommand sqlCommand = new SqlCommand("insert into [dbo].[KitCategory]([name]) values(@name); select scope_identity();"))
         {
            sqlCommand.Parameters.AddWithValue("@name", kitCategory.Name);

            return Convert.ToInt32(Commands.RetrieveScalarValue(sqlCommand));
         }
      }

      public static void Delete(int kitCategoryId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("delete [dbo].[KitCategory] where [kitcategoryid]=@kitcategoryid;"))
         {
            sqlCommand.Parameters.AddWithValue("@kitcategoryid", kitCategoryId);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      public static Models.KitCategory? Retrieve(int kitCategoryId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("select [kitcategoryid],[name] from [dbo].[KitCategory] where [kitcategoryid]=@kitcategoryid;"))
         {
            sqlCommand.Parameters.AddWithValue("@kitcategoryid", kitCategoryId);

            return PopulateKitCategoryModel(Commands.RetrieveDataRow(sqlCommand));
         }
      }

      public static void Update(Models.KitCategory kitCategory)
      {
         using (SqlCommand sqlCommand = new SqlCommand("update [dbo].[KitCategory] set [name]=@name where [kitcategoryid]=@kitcategoryid;"))
         {
            sqlCommand.Parameters.AddWithValue("@kitcategoryid", kitCategory.KitCategoryId);
            sqlCommand.Parameters.AddWithValue("@name", kitCategory.Name);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      private static Models.KitCategory? PopulateKitCategoryModel(DataRow dataRow)
      {
         if (dataRow != null)
         {
            return new Models.KitCategory {
                                             KitCategoryId = Convert.ToInt32(dataRow["kitcategoryid"]), 
                                             Name = Convert.ToString(dataRow["name"]), 
                                          };
         }

         return null;
      }
   }

   [ExcludeFromCodeCoverage]
   [DebuggerStepThrough]
   public static class KitForSaleService
   {
      public static List<Models.KitForSale> RetrieveAll
      {
         get
         {
            List<Models.KitForSale> kitForSaleList = new List<Models.KitForSale>();

            using (SqlCommand sqlCommand = new SqlCommand("select [kitforsaleid],[name],[description],[memberid],[kitcategoryid],[minprice],[maxprice],[created],[modified] from [dbo].[KitForSale];"))
            {
               Models.KitForSale? kitForSale;
               foreach (DataRow dataRow in Commands.RetrieveDataTable(sqlCommand).Rows)
               {
                  kitForSale = PopulateKitForSaleModel(dataRow);
                  if (kitForSale != null)
                  {
                     kitForSaleList.Add((Models.KitForSale)kitForSale);
                  }
               }
            }

            return kitForSaleList;
         }
      }

      public static int Create(Models.KitForSale kitForSale)
      {
         using (SqlCommand sqlCommand = new SqlCommand("insert into [dbo].[KitForSale]([name],[description],[memberid],[kitcategoryid],[minprice],[maxprice],[created],[modified]) values(@name,@description,@memberid,@kitcategoryid,@minprice,@maxprice,@created,@modified); select scope_identity();"))
         {
            sqlCommand.Parameters.AddWithValue("@name", kitForSale.Name);
            sqlCommand.Parameters.AddWithValue("@description", kitForSale.Description);
            sqlCommand.Parameters.AddWithValue("@memberid", kitForSale.MemberId);
            sqlCommand.Parameters.AddWithValue("@kitcategoryid", kitForSale.KitCategoryId);
            sqlCommand.Parameters.AddWithValue("@minprice", kitForSale.MinPrice);
            sqlCommand.Parameters.AddWithValue("@maxprice", kitForSale.MaXPrice);
            sqlCommand.Parameters.AddWithValue("@created", kitForSale.Created);
            sqlCommand.Parameters.AddWithValue("@modified", kitForSale.Modified);

            return Convert.ToInt32(Commands.RetrieveScalarValue(sqlCommand));
         }
      }

      public static void Delete(int kitForSaleId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("delete [dbo].[KitForSale] where [kitforsaleid]=@kitforsaleid;"))
         {
            sqlCommand.Parameters.AddWithValue("@kitforsaleid", kitForSaleId);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      public static Models.KitForSale? Retrieve(int kitForSaleId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("select [kitforsaleid],[name],[description],[memberid],[kitcategoryid],[minprice],[maxprice],[created],[modified] from [dbo].[KitForSale] where [kitforsaleid]=@kitforsaleid;"))
         {
            sqlCommand.Parameters.AddWithValue("@kitforsaleid", kitForSaleId);

            return PopulateKitForSaleModel(Commands.RetrieveDataRow(sqlCommand));
         }
      }

      public static void Update(Models.KitForSale kitForSale)
      {
         using (SqlCommand sqlCommand = new SqlCommand("update [dbo].[KitForSale] set [name]=@name,[description]=@description,[memberid]=@memberid,[kitcategoryid]=@kitcategoryid,[minprice]=@minprice,[maxprice]=@maxprice,[created]=@created,[modified]=@modified where [kitforsaleid]=@kitforsaleid;"))
         {
            sqlCommand.Parameters.AddWithValue("@kitforsaleid", kitForSale.KitForSaleId);
            sqlCommand.Parameters.AddWithValue("@name", kitForSale.Name);
            sqlCommand.Parameters.AddWithValue("@description", kitForSale.Description);
            sqlCommand.Parameters.AddWithValue("@memberid", kitForSale.MemberId);
            sqlCommand.Parameters.AddWithValue("@kitcategoryid", kitForSale.KitCategoryId);
            sqlCommand.Parameters.AddWithValue("@minprice", kitForSale.MinPrice);
            sqlCommand.Parameters.AddWithValue("@maxprice", kitForSale.MaXPrice);
            sqlCommand.Parameters.AddWithValue("@created", kitForSale.Created);
            sqlCommand.Parameters.AddWithValue("@modified", kitForSale.Modified);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      private static Models.KitForSale? PopulateKitForSaleModel(DataRow dataRow)
      {
         if (dataRow != null)
         {
            return new Models.KitForSale {
                                            KitForSaleId = Convert.ToInt32(dataRow["kitforsaleid"]), 
                                            Name = Convert.ToString(dataRow["name"]), 
                                            Description = Convert.ToString(dataRow["description"]), 
                                            MemberId = Convert.ToInt32(dataRow["memberid"]), 
                                            KitCategoryId = Convert.ToInt32(dataRow["kitcategoryid"]), 
                                            MinPrice = Convert.ToDecimal(dataRow["minprice"]), 
                                            MaXPrice = Convert.ToDecimal(dataRow["maxprice"]), 
                                            Created = Convert.ToDateTime(dataRow["created"]), 
                                            Modified = Convert.ToDateTime(dataRow["modified"]), 
                                         };
         }

         return null;
      }
   }

   [ExcludeFromCodeCoverage]
   [DebuggerStepThrough]
   public static class MemberService
   {
      public static List<Models.Member> RetrieveAll
      {
         get
         {
            List<Models.Member> memberList = new List<Models.Member>();

            using (SqlCommand sqlCommand = new SqlCommand("select [memberid],[forename],[surname],[initials],[emailaddress1],[emailaddress2],[homephonenumber],[mobilephonenumber],[address1],[address2],[town],[county],[postcode],[emergencycontactphonenumber],[emergencycontactname],[emergencycontactrelationship],[divergradeid],[chairman],[divingofficer],[trainingofficer],[secretary],[treasurer],[socialsecretary],[equipmentofficer],[administrator],[username],[password],[bsacmembershipnumber],[notes],[sessionguid],[lastactivity],[usecommaasaddressseparator],[isdeleted],[instructor],[assistantinstructor],[o2admin],[firstaider],[hidemember] from [dbo].[Member];"))
            {
               Models.Member? member;
               foreach (DataRow dataRow in Commands.RetrieveDataTable(sqlCommand).Rows)
               {
                  member = PopulateMemberModel(dataRow);
                  if (member != null)
                  {
                     memberList.Add((Models.Member)member);
                  }
               }
            }

            return memberList;
         }
      }

      public static int Create(Models.Member member)
      {
         using (SqlCommand sqlCommand = new SqlCommand("insert into [dbo].[Member]([forename],[surname],[initials],[emailaddress1],[emailaddress2],[homephonenumber],[mobilephonenumber],[address1],[address2],[town],[county],[postcode],[emergencycontactphonenumber],[emergencycontactname],[emergencycontactrelationship],[divergradeid],[chairman],[divingofficer],[trainingofficer],[secretary],[treasurer],[socialsecretary],[equipmentofficer],[administrator],[username],[password],[bsacmembershipnumber],[notes],[sessionguid],[lastactivity],[usecommaasaddressseparator],[isdeleted],[instructor],[assistantinstructor],[o2admin],[firstaider],[hidemember]) values(@forename,@surname,@initials,@emailaddress1,@emailaddress2,@homephonenumber,@mobilephonenumber,@address1,@address2,@town,@county,@postcode,@emergencycontactphonenumber,@emergencycontactname,@emergencycontactrelationship,@divergradeid,@chairman,@divingofficer,@trainingofficer,@secretary,@treasurer,@socialsecretary,@equipmentofficer,@administrator,@username,@password,@bsacmembershipnumber,@notes,@sessionguid,@lastactivity,@usecommaasaddressseparator,@isdeleted,@instructor,@assistantinstructor,@o2admin,@firstaider,@hidemember); select scope_identity();"))
         {
            sqlCommand.Parameters.AddWithValue("@forename", member.Forename);
            sqlCommand.Parameters.AddWithValue("@surname", member.Surname);
            sqlCommand.Parameters.AddWithValue("@initials", member.Initials);
            sqlCommand.Parameters.AddWithValue("@emailaddress1", member.EmailAddress1);
            sqlCommand.Parameters.AddWithValue("@emailaddress2", member.EmailAddress2);
            sqlCommand.Parameters.AddWithValue("@homephonenumber", member.HomePhoneNumber);
            sqlCommand.Parameters.AddWithValue("@mobilephonenumber", member.MobilePhoneNumber);
            sqlCommand.Parameters.AddWithValue("@address1", member.Address1);
            sqlCommand.Parameters.AddWithValue("@address2", member.Address2);
            sqlCommand.Parameters.AddWithValue("@town", member.Town);
            sqlCommand.Parameters.AddWithValue("@county", member.County);
            sqlCommand.Parameters.AddWithValue("@postcode", member.Postcode);
            sqlCommand.Parameters.AddWithValue("@emergencycontactphonenumber", member.EmergencyContactPhoneNumber);
            sqlCommand.Parameters.AddWithValue("@emergencycontactname", member.EmergencyContactName);
            sqlCommand.Parameters.AddWithValue("@emergencycontactrelationship", member.EmergencyContactRelationship);
            sqlCommand.Parameters.AddWithValue("@divergradeid", member.DiverGradeId);
            sqlCommand.Parameters.AddWithValue("@chairman", member.Chairman);
            sqlCommand.Parameters.AddWithValue("@divingofficer", member.DivingOfficer);
            sqlCommand.Parameters.AddWithValue("@trainingofficer", member.TrainingOfficer);
            sqlCommand.Parameters.AddWithValue("@secretary", member.Secretary);
            sqlCommand.Parameters.AddWithValue("@treasurer", member.Treasurer);
            sqlCommand.Parameters.AddWithValue("@socialsecretary", member.SocialSecretary);
            sqlCommand.Parameters.AddWithValue("@equipmentofficer", member.EquipmentOfficer);
            sqlCommand.Parameters.AddWithValue("@administrator", member.Administrator);
            sqlCommand.Parameters.AddWithValue("@username", member.UserName);
            sqlCommand.Parameters.AddWithValue("@password", member.Password);
            sqlCommand.Parameters.AddWithValue("@bsacmembershipnumber", member.BsaCMembershipNumber);
            sqlCommand.Parameters.AddWithValue("@notes", member.Notes);
            sqlCommand.Parameters.AddWithValue("@sessionguid", member.SessionGuiD);
            sqlCommand.Parameters.AddWithValue("@lastactivity", member.LastActivity);
            sqlCommand.Parameters.AddWithValue("@usecommaasaddressseparator", member.UseCommaAsAddressSeparator);
            sqlCommand.Parameters.AddWithValue("@isdeleted", member.IsDeleted);
            sqlCommand.Parameters.AddWithValue("@instructor", member.Instructor);
            sqlCommand.Parameters.AddWithValue("@assistantinstructor", member.AssistantInstructor);
            sqlCommand.Parameters.AddWithValue("@o2admin", member.O2AdMin);
            sqlCommand.Parameters.AddWithValue("@firstaider", member.FirstAider);
            sqlCommand.Parameters.AddWithValue("@hidemember", member.HideMember);

            return Convert.ToInt32(Commands.RetrieveScalarValue(sqlCommand));
         }
      }

      public static void Delete(int memberId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("delete [dbo].[Member] where [memberid]=@memberid;"))
         {
            sqlCommand.Parameters.AddWithValue("@memberid", memberId);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      public static Models.Member? Retrieve(int memberId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("select [memberid],[forename],[surname],[initials],[emailaddress1],[emailaddress2],[homephonenumber],[mobilephonenumber],[address1],[address2],[town],[county],[postcode],[emergencycontactphonenumber],[emergencycontactname],[emergencycontactrelationship],[divergradeid],[chairman],[divingofficer],[trainingofficer],[secretary],[treasurer],[socialsecretary],[equipmentofficer],[administrator],[username],[password],[bsacmembershipnumber],[notes],[sessionguid],[lastactivity],[usecommaasaddressseparator],[isdeleted],[instructor],[assistantinstructor],[o2admin],[firstaider],[hidemember] from [dbo].[Member] where [memberid]=@memberid;"))
         {
            sqlCommand.Parameters.AddWithValue("@memberid", memberId);

            return PopulateMemberModel(Commands.RetrieveDataRow(sqlCommand));
         }
      }

      public static void Update(Models.Member member)
      {
         using (SqlCommand sqlCommand = new SqlCommand("update [dbo].[Member] set [forename]=@forename,[surname]=@surname,[initials]=@initials,[emailaddress1]=@emailaddress1,[emailaddress2]=@emailaddress2,[homephonenumber]=@homephonenumber,[mobilephonenumber]=@mobilephonenumber,[address1]=@address1,[address2]=@address2,[town]=@town,[county]=@county,[postcode]=@postcode,[emergencycontactphonenumber]=@emergencycontactphonenumber,[emergencycontactname]=@emergencycontactname,[emergencycontactrelationship]=@emergencycontactrelationship,[divergradeid]=@divergradeid,[chairman]=@chairman,[divingofficer]=@divingofficer,[trainingofficer]=@trainingofficer,[secretary]=@secretary,[treasurer]=@treasurer,[socialsecretary]=@socialsecretary,[equipmentofficer]=@equipmentofficer,[administrator]=@administrator,[username]=@username,[password]=@password,[bsacmembershipnumber]=@bsacmembershipnumber,[notes]=@notes,[sessionguid]=@sessionguid,[lastactivity]=@lastactivity,[usecommaasaddressseparator]=@usecommaasaddressseparator,[isdeleted]=@isdeleted,[instructor]=@instructor,[assistantinstructor]=@assistantinstructor,[o2admin]=@o2admin,[firstaider]=@firstaider,[hidemember]=@hidemember where [memberid]=@memberid;"))
         {
            sqlCommand.Parameters.AddWithValue("@memberid", member.MemberId);
            sqlCommand.Parameters.AddWithValue("@forename", member.Forename);
            sqlCommand.Parameters.AddWithValue("@surname", member.Surname);
            sqlCommand.Parameters.AddWithValue("@initials", member.Initials);
            sqlCommand.Parameters.AddWithValue("@emailaddress1", member.EmailAddress1);
            sqlCommand.Parameters.AddWithValue("@emailaddress2", member.EmailAddress2);
            sqlCommand.Parameters.AddWithValue("@homephonenumber", member.HomePhoneNumber);
            sqlCommand.Parameters.AddWithValue("@mobilephonenumber", member.MobilePhoneNumber);
            sqlCommand.Parameters.AddWithValue("@address1", member.Address1);
            sqlCommand.Parameters.AddWithValue("@address2", member.Address2);
            sqlCommand.Parameters.AddWithValue("@town", member.Town);
            sqlCommand.Parameters.AddWithValue("@county", member.County);
            sqlCommand.Parameters.AddWithValue("@postcode", member.Postcode);
            sqlCommand.Parameters.AddWithValue("@emergencycontactphonenumber", member.EmergencyContactPhoneNumber);
            sqlCommand.Parameters.AddWithValue("@emergencycontactname", member.EmergencyContactName);
            sqlCommand.Parameters.AddWithValue("@emergencycontactrelationship", member.EmergencyContactRelationship);
            sqlCommand.Parameters.AddWithValue("@divergradeid", member.DiverGradeId);
            sqlCommand.Parameters.AddWithValue("@chairman", member.Chairman);
            sqlCommand.Parameters.AddWithValue("@divingofficer", member.DivingOfficer);
            sqlCommand.Parameters.AddWithValue("@trainingofficer", member.TrainingOfficer);
            sqlCommand.Parameters.AddWithValue("@secretary", member.Secretary);
            sqlCommand.Parameters.AddWithValue("@treasurer", member.Treasurer);
            sqlCommand.Parameters.AddWithValue("@socialsecretary", member.SocialSecretary);
            sqlCommand.Parameters.AddWithValue("@equipmentofficer", member.EquipmentOfficer);
            sqlCommand.Parameters.AddWithValue("@administrator", member.Administrator);
            sqlCommand.Parameters.AddWithValue("@username", member.UserName);
            sqlCommand.Parameters.AddWithValue("@password", member.Password);
            sqlCommand.Parameters.AddWithValue("@bsacmembershipnumber", member.BsaCMembershipNumber);
            sqlCommand.Parameters.AddWithValue("@notes", member.Notes);
            sqlCommand.Parameters.AddWithValue("@sessionguid", member.SessionGuiD);
            sqlCommand.Parameters.AddWithValue("@lastactivity", member.LastActivity);
            sqlCommand.Parameters.AddWithValue("@usecommaasaddressseparator", member.UseCommaAsAddressSeparator);
            sqlCommand.Parameters.AddWithValue("@isdeleted", member.IsDeleted);
            sqlCommand.Parameters.AddWithValue("@instructor", member.Instructor);
            sqlCommand.Parameters.AddWithValue("@assistantinstructor", member.AssistantInstructor);
            sqlCommand.Parameters.AddWithValue("@o2admin", member.O2AdMin);
            sqlCommand.Parameters.AddWithValue("@firstaider", member.FirstAider);
            sqlCommand.Parameters.AddWithValue("@hidemember", member.HideMember);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      private static Models.Member? PopulateMemberModel(DataRow dataRow)
      {
         if (dataRow != null)
         {
            return new Models.Member {
                                        MemberId = Convert.ToInt32(dataRow["memberid"]), 
                                        Forename = Convert.ToString(dataRow["forename"]), 
                                        Surname = Convert.ToString(dataRow["surname"]), 
                                        Initials = Convert.ToString(dataRow["initials"]), 
                                        EmailAddress1 = Convert.ToString(dataRow["emailaddress1"]), 
                                        EmailAddress2 = Convert.ToString(dataRow["emailaddress2"]), 
                                        HomePhoneNumber = Convert.ToString(dataRow["homephonenumber"]), 
                                        MobilePhoneNumber = Convert.ToString(dataRow["mobilephonenumber"]), 
                                        Address1 = Convert.ToString(dataRow["address1"]), 
                                        Address2 = Convert.ToString(dataRow["address2"]), 
                                        Town = Convert.ToString(dataRow["town"]), 
                                        County = Convert.ToString(dataRow["county"]), 
                                        Postcode = Convert.ToString(dataRow["postcode"]), 
                                        EmergencyContactPhoneNumber = Convert.ToString(dataRow["emergencycontactphonenumber"]), 
                                        EmergencyContactName = Convert.ToString(dataRow["emergencycontactname"]), 
                                        EmergencyContactRelationship = Convert.ToString(dataRow["emergencycontactrelationship"]), 
                                        DiverGradeId = Convert.ToInt32(dataRow["divergradeid"]), 
                                        Chairman = Convert.ToBoolean(dataRow["chairman"]), 
                                        DivingOfficer = Convert.ToBoolean(dataRow["divingofficer"]), 
                                        TrainingOfficer = Convert.ToBoolean(dataRow["trainingofficer"]), 
                                        Secretary = Convert.ToBoolean(dataRow["secretary"]), 
                                        Treasurer = Convert.ToBoolean(dataRow["treasurer"]), 
                                        SocialSecretary = Convert.ToBoolean(dataRow["socialsecretary"]), 
                                        EquipmentOfficer = Convert.ToBoolean(dataRow["equipmentofficer"]), 
                                        Administrator = Convert.ToBoolean(dataRow["administrator"]), 
                                        UserName = Convert.ToString(dataRow["username"]), 
                                        Password = Convert.ToString(dataRow["password"]), 
                                        BsaCMembershipNumber = Convert.ToString(dataRow["bsacmembershipnumber"]), 
                                        Notes = Convert.ToString(dataRow["notes"]), 
                                        SessionGuiD = Convert.ToString(dataRow["sessionguid"]), 
                                        LastActivity = Convert.ToDateTime(dataRow["lastactivity"]), 
                                        UseCommaAsAddressSeparator = Convert.ToBoolean(dataRow["usecommaasaddressseparator"]), 
                                        IsDeleted = Convert.ToBoolean(dataRow["isdeleted"]), 
                                        Instructor = Convert.ToBoolean(dataRow["instructor"]), 
                                        AssistantInstructor = Convert.ToBoolean(dataRow["assistantinstructor"]), 
                                        O2AdMin = Convert.ToBoolean(dataRow["o2admin"]), 
                                        FirstAider = Convert.ToBoolean(dataRow["firstaider"]), 
                                        HideMember = Convert.ToBoolean(dataRow["hidemember"]), 
                                     };
         }

         return null;
      }
   }

   [ExcludeFromCodeCoverage]
   [DebuggerStepThrough]
   public static class SignOffService
   {
      public static List<Models.SignOff> RetrieveAll
      {
         get
         {
            List<Models.SignOff> signOffList = new List<Models.SignOff>();

            using (SqlCommand sqlCommand = new SqlCommand("select [SignOffId],[DiverGradeId],[SignOffTypeId],[ModuleCode],[Name],[DisplayOrder] from [dbo].[SignOff];"))
            {
               Models.SignOff? signOff;
               foreach (DataRow dataRow in Commands.RetrieveDataTable(sqlCommand).Rows)
               {
                  signOff = PopulateSignOffModel(dataRow);
                  if (signOff != null)
                  {
                     signOffList.Add((Models.SignOff)signOff);
                  }
               }
            }

            return signOffList;
         }
      }

      public static int Create(Models.SignOff signOff)
      {
         using (SqlCommand sqlCommand = new SqlCommand("insert into [dbo].[SignOff]([DiverGradeId],[SignOffTypeId],[ModuleCode],[Name],[DisplayOrder]) values(@DiverGradeId,@SignOffTypeId,@ModuleCode,@Name,@DisplayOrder); select scope_identity();"))
         {
            sqlCommand.Parameters.AddWithValue("@DiverGradeId", signOff.DiverGradeId);
            sqlCommand.Parameters.AddWithValue("@SignOffTypeId", signOff.SignOffType);
            sqlCommand.Parameters.AddWithValue("@ModuleCode", signOff.ModuleCode);
            sqlCommand.Parameters.AddWithValue("@Name", signOff.Name);
            sqlCommand.Parameters.AddWithValue("@DisplayOrder", signOff.DisplayOrder);

            return Convert.ToInt32(Commands.RetrieveScalarValue(sqlCommand));
         }
      }

      public static void Delete(int signOffId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("delete [dbo].[SignOff] where [SignOffId]=@SignOffId;"))
         {
            sqlCommand.Parameters.AddWithValue("@SignOffId", signOffId);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      public static Models.SignOff? Retrieve(int signOffId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("select [SignOffId],[DiverGradeId],[SignOffTypeId],[ModuleCode],[Name],[DisplayOrder] from [dbo].[SignOff] where [SignOffId]=@SignOffId;"))
         {
            sqlCommand.Parameters.AddWithValue("@SignOffId", signOffId);

            return PopulateSignOffModel(Commands.RetrieveDataRow(sqlCommand));
         }
      }

      public static void Update(Models.SignOff signOff)
      {
         using (SqlCommand sqlCommand = new SqlCommand("update [dbo].[SignOff] set [DiverGradeId]=@DiverGradeId,[SignOffTypeId]=@SignOffTypeId,[ModuleCode]=@ModuleCode,[Name]=@Name,[DisplayOrder]=@DisplayOrder where [SignOffId]=@SignOffId;"))
         {
            sqlCommand.Parameters.AddWithValue("@SignOffId", signOff.SignOffId);
            sqlCommand.Parameters.AddWithValue("@DiverGradeId", signOff.DiverGradeId);
            sqlCommand.Parameters.AddWithValue("@SignOffTypeId", signOff.SignOffType);
            sqlCommand.Parameters.AddWithValue("@ModuleCode", signOff.ModuleCode);
            sqlCommand.Parameters.AddWithValue("@Name", signOff.Name);
            sqlCommand.Parameters.AddWithValue("@DisplayOrder", signOff.DisplayOrder);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      private static Models.SignOff? PopulateSignOffModel(DataRow dataRow)
      {
         if (dataRow != null)
         {
            return new Models.SignOff {
                                         SignOffId = Convert.ToInt32(dataRow["SignOffId"]), 
                                         DiverGradeId = Convert.ToInt32(dataRow["DiverGradeId"]), 
                                         SignOffType = (Models.SignOffTypeEnum)Convert.ToInt32(dataRow["SignOffTypeId"]), 
                                         ModuleCode = Convert.ToString(dataRow["ModuleCode"]), 
                                         Name = Convert.ToString(dataRow["Name"]), 
                                         DisplayOrder = Convert.ToByte(dataRow["DisplayOrder"]), 
                                      };
         }

         return null;
      }
   }

   [ExcludeFromCodeCoverage]
   [DebuggerStepThrough]
   public static class SignOffRecordService
   {
      public static List<Models.SignOffRecord> RetrieveAll
      {
         get
         {
            List<Models.SignOffRecord> signOffRecordList = new List<Models.SignOffRecord>();

            using (SqlCommand sqlCommand = new SqlCommand("select [SignOffRecordId],[Memberid],[SignOffId],[SignoffDate],[SignedOffByMemberId],[Notes] from [dbo].[SignOffRecord];"))
            {
               Models.SignOffRecord? signOffRecord;
               foreach (DataRow dataRow in Commands.RetrieveDataTable(sqlCommand).Rows)
               {
                  signOffRecord = PopulateSignOffRecordModel(dataRow);
                  if (signOffRecord != null)
                  {
                     signOffRecordList.Add((Models.SignOffRecord)signOffRecord);
                  }
               }
            }

            return signOffRecordList;
         }
      }

      public static int Create(Models.SignOffRecord signOffRecord)
      {
         using (SqlCommand sqlCommand = new SqlCommand("insert into [dbo].[SignOffRecord]([Memberid],[SignOffId],[SignoffDate],[SignedOffByMemberId],[Notes]) values(@Memberid,@SignOffId,@SignoffDate,@SignedOffByMemberId,@Notes); select scope_identity();"))
         {
            sqlCommand.Parameters.AddWithValue("@Memberid", signOffRecord.MemberId);
            sqlCommand.Parameters.AddWithValue("@SignOffId", signOffRecord.SignOffId);
            sqlCommand.Parameters.AddWithValue("@SignoffDate", signOffRecord.SignOffDate);
            sqlCommand.Parameters.AddWithValue("@SignedOffByMemberId", signOffRecord.SignedOffByMemberId);
            sqlCommand.Parameters.AddWithValue("@Notes", signOffRecord.Notes);

            return Convert.ToInt32(Commands.RetrieveScalarValue(sqlCommand));
         }
      }

      public static void Delete(int signOffRecordId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("delete [dbo].[SignOffRecord] where [SignOffRecordId]=@SignOffRecordId;"))
         {
            sqlCommand.Parameters.AddWithValue("@SignOffRecordId", signOffRecordId);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      public static Models.SignOffRecord? Retrieve(int signOffRecordId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("select [SignOffRecordId],[Memberid],[SignOffId],[SignoffDate],[SignedOffByMemberId],[Notes] from [dbo].[SignOffRecord] where [SignOffRecordId]=@SignOffRecordId;"))
         {
            sqlCommand.Parameters.AddWithValue("@SignOffRecordId", signOffRecordId);

            return PopulateSignOffRecordModel(Commands.RetrieveDataRow(sqlCommand));
         }
      }

      public static void Update(Models.SignOffRecord signOffRecord)
      {
         using (SqlCommand sqlCommand = new SqlCommand("update [dbo].[SignOffRecord] set [Memberid]=@Memberid,[SignOffId]=@SignOffId,[SignoffDate]=@SignoffDate,[SignedOffByMemberId]=@SignedOffByMemberId,[Notes]=@Notes where [SignOffRecordId]=@SignOffRecordId;"))
         {
            sqlCommand.Parameters.AddWithValue("@SignOffRecordId", signOffRecord.SignOffRecordId);
            sqlCommand.Parameters.AddWithValue("@Memberid", signOffRecord.MemberId);
            sqlCommand.Parameters.AddWithValue("@SignOffId", signOffRecord.SignOffId);
            sqlCommand.Parameters.AddWithValue("@SignoffDate", signOffRecord.SignOffDate);
            sqlCommand.Parameters.AddWithValue("@SignedOffByMemberId", signOffRecord.SignedOffByMemberId);
            sqlCommand.Parameters.AddWithValue("@Notes", signOffRecord.Notes);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      private static Models.SignOffRecord? PopulateSignOffRecordModel(DataRow dataRow)
      {
         if (dataRow != null)
         {
            return new Models.SignOffRecord {
                                               SignOffRecordId = Convert.ToInt32(dataRow["SignOffRecordId"]), 
                                               MemberId = Convert.ToInt32(dataRow["Memberid"]), 
                                               SignOffId = Convert.ToInt32(dataRow["SignOffId"]), 
                                               SignOffDate = Convert.ToDateTime(dataRow["SignoffDate"]), 
                                               SignedOffByMemberId = Convert.ToInt32(dataRow["SignedOffByMemberId"]), 
                                               Notes = Convert.ToString(dataRow["Notes"]), 
                                            };
         }

         return null;
      }
   }

   [ExcludeFromCodeCoverage]
   [DebuggerStepThrough]
   public static class UploadedFileService
   {
      public static List<Models.UploadedFile> RetrieveAll
      {
         get
         {
            List<Models.UploadedFile> uploadedFileList = new List<Models.UploadedFile>();

            using (SqlCommand sqlCommand = new SqlCommand("select [uploadedfileid],[originalfilename],[archivefilename],[imagewidth],[imageheight],[contenttype],[description],[notes],[created],[modifiedbymemberid],[modified],[size],[name],[createdbymemberid],[thumbnailfilename] from [dbo].[UploadedFile];"))
            {
               Models.UploadedFile? uploadedFile;
               foreach (DataRow dataRow in Commands.RetrieveDataTable(sqlCommand).Rows)
               {
                  uploadedFile = PopulateUploadedFileModel(dataRow);
                  if (uploadedFile != null)
                  {
                     uploadedFileList.Add((Models.UploadedFile)uploadedFile);
                  }
               }
            }

            return uploadedFileList;
         }
      }

      public static int Create(Models.UploadedFile uploadedFile)
      {
         using (SqlCommand sqlCommand = new SqlCommand("insert into [dbo].[UploadedFile]([originalfilename],[archivefilename],[imagewidth],[imageheight],[contenttype],[description],[notes],[created],[modifiedbymemberid],[modified],[size],[name],[createdbymemberid],[thumbnailfilename]) values(@originalfilename,@archivefilename,@imagewidth,@imageheight,@contenttype,@description,@notes,@created,@modifiedbymemberid,@modified,@size,@name,@createdbymemberid,@thumbnailfilename); select scope_identity();"))
         {
            sqlCommand.Parameters.AddWithValue("@originalfilename", uploadedFile.OriginalFileName);
            sqlCommand.Parameters.AddWithValue("@archivefilename", uploadedFile.ArchiveFileName);
            sqlCommand.Parameters.AddWithValue("@imagewidth", uploadedFile.ImageWidth);
            sqlCommand.Parameters.AddWithValue("@imageheight", uploadedFile.ImageHeight);
            sqlCommand.Parameters.AddWithValue("@contenttype", uploadedFile.ContentType);
            sqlCommand.Parameters.AddWithValue("@description", uploadedFile.Description);
            sqlCommand.Parameters.AddWithValue("@notes", uploadedFile.Notes);
            sqlCommand.Parameters.AddWithValue("@created", uploadedFile.Created);
            sqlCommand.Parameters.AddWithValue("@modifiedbymemberid", uploadedFile.ModifiedByMemberId);
            sqlCommand.Parameters.AddWithValue("@modified", uploadedFile.Modified);
            sqlCommand.Parameters.AddWithValue("@size", uploadedFile.Size);
            sqlCommand.Parameters.AddWithValue("@name", uploadedFile.Name);
            sqlCommand.Parameters.AddWithValue("@createdbymemberid", uploadedFile.CreatedByMemberId);
            sqlCommand.Parameters.AddWithValue("@thumbnailfilename", uploadedFile.ThumbnailFileName);

            return Convert.ToInt32(Commands.RetrieveScalarValue(sqlCommand));
         }
      }

      public static void Delete(int uploadedFileId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("delete [dbo].[UploadedFile] where [uploadedfileid]=@uploadedfileid;"))
         {
            sqlCommand.Parameters.AddWithValue("@uploadedfileid", uploadedFileId);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      public static Models.UploadedFile? Retrieve(int uploadedFileId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("select [uploadedfileid],[originalfilename],[archivefilename],[imagewidth],[imageheight],[contenttype],[description],[notes],[created],[modifiedbymemberid],[modified],[size],[name],[createdbymemberid],[thumbnailfilename] from [dbo].[UploadedFile] where [uploadedfileid]=@uploadedfileid;"))
         {
            sqlCommand.Parameters.AddWithValue("@uploadedfileid", uploadedFileId);

            return PopulateUploadedFileModel(Commands.RetrieveDataRow(sqlCommand));
         }
      }

      public static void Update(Models.UploadedFile uploadedFile)
      {
         using (SqlCommand sqlCommand = new SqlCommand("update [dbo].[UploadedFile] set [originalfilename]=@originalfilename,[archivefilename]=@archivefilename,[imagewidth]=@imagewidth,[imageheight]=@imageheight,[contenttype]=@contenttype,[description]=@description,[notes]=@notes,[created]=@created,[modifiedbymemberid]=@modifiedbymemberid,[modified]=@modified,[size]=@size,[name]=@name,[createdbymemberid]=@createdbymemberid,[thumbnailfilename]=@thumbnailfilename where [uploadedfileid]=@uploadedfileid;"))
         {
            sqlCommand.Parameters.AddWithValue("@uploadedfileid", uploadedFile.UploadedFileId);
            sqlCommand.Parameters.AddWithValue("@originalfilename", uploadedFile.OriginalFileName);
            sqlCommand.Parameters.AddWithValue("@archivefilename", uploadedFile.ArchiveFileName);
            sqlCommand.Parameters.AddWithValue("@imagewidth", uploadedFile.ImageWidth);
            sqlCommand.Parameters.AddWithValue("@imageheight", uploadedFile.ImageHeight);
            sqlCommand.Parameters.AddWithValue("@contenttype", uploadedFile.ContentType);
            sqlCommand.Parameters.AddWithValue("@description", uploadedFile.Description);
            sqlCommand.Parameters.AddWithValue("@notes", uploadedFile.Notes);
            sqlCommand.Parameters.AddWithValue("@created", uploadedFile.Created);
            sqlCommand.Parameters.AddWithValue("@modifiedbymemberid", uploadedFile.ModifiedByMemberId);
            sqlCommand.Parameters.AddWithValue("@modified", uploadedFile.Modified);
            sqlCommand.Parameters.AddWithValue("@size", uploadedFile.Size);
            sqlCommand.Parameters.AddWithValue("@name", uploadedFile.Name);
            sqlCommand.Parameters.AddWithValue("@createdbymemberid", uploadedFile.CreatedByMemberId);
            sqlCommand.Parameters.AddWithValue("@thumbnailfilename", uploadedFile.ThumbnailFileName);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      private static Models.UploadedFile? PopulateUploadedFileModel(DataRow dataRow)
      {
         if (dataRow != null)
         {
            return new Models.UploadedFile {
                                              UploadedFileId = Convert.ToInt32(dataRow["uploadedfileid"]), 
                                              OriginalFileName = Convert.ToString(dataRow["originalfilename"]), 
                                              ArchiveFileName = Convert.ToString(dataRow["archivefilename"]), 
                                              ImageWidth = Convert.ToInt32(dataRow["imagewidth"]), 
                                              ImageHeight = Convert.ToInt32(dataRow["imageheight"]), 
                                              ContentType = Convert.ToString(dataRow["contenttype"]), 
                                              Description = Convert.ToString(dataRow["description"]), 
                                              Notes = Convert.ToString(dataRow["notes"]), 
                                              Created = Convert.ToDateTime(dataRow["created"]), 
                                              ModifiedByMemberId = Convert.ToInt32(dataRow["modifiedbymemberid"]), 
                                              Modified = Convert.ToDateTime(dataRow["modified"]), 
                                              Size = Convert.ToInt32(dataRow["size"]), 
                                              Name = Convert.ToString(dataRow["name"]), 
                                              CreatedByMemberId = Convert.ToInt32(dataRow["createdbymemberid"]), 
                                              ThumbnailFileName = Convert.ToString(dataRow["thumbnailfilename"]), 
                                           };
         }

         return null;
      }
   }

   [ExcludeFromCodeCoverage]
   [DebuggerStepThrough]
   public static class WebsiteService
   {
      public static List<Models.Website> RetrieveAll
      {
         get
         {
            List<Models.Website> websiteList = new List<Models.Website>();

            using (SqlCommand sqlCommand = new SqlCommand("select [WebsiteId],[SiteHitCount],[SiteLastHit] from [dbo].[Website];"))
            {
               Models.Website? website;
               foreach (DataRow dataRow in Commands.RetrieveDataTable(sqlCommand).Rows)
               {
                  website = PopulateWebsiteModel(dataRow);
                  if (website != null)
                  {
                     websiteList.Add((Models.Website)website);
                  }
               }
            }

            return websiteList;
         }
      }

      public static int Create(Models.Website website)
      {
         using (SqlCommand sqlCommand = new SqlCommand("insert into [dbo].[Website]([WebsiteId],[SiteHitCount],[SiteLastHit]) values(@WebsiteId,@SiteHitCount,@SiteLastHit); select scope_identity();"))
         {
            sqlCommand.Parameters.AddWithValue("@WebsiteId", website.WebsiteId);
            sqlCommand.Parameters.AddWithValue("@SiteHitCount", website.SiteHitCount);
            sqlCommand.Parameters.AddWithValue("@SiteLastHit", website.SiteLastHit);

            return Convert.ToInt32(Commands.RetrieveScalarValue(sqlCommand));
         }
      }

      public static void Delete(int websiteId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("delete [dbo].[Website] where [WebsiteId]=@WebsiteId;"))
         {
            sqlCommand.Parameters.AddWithValue("@WebsiteId", websiteId);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      public static Models.Website? Retrieve(int websiteId)
      {
         using (SqlCommand sqlCommand = new SqlCommand("select [WebsiteId],[SiteHitCount],[SiteLastHit] from [dbo].[Website] where [WebsiteId]=@WebsiteId;"))
         {
            sqlCommand.Parameters.AddWithValue("@WebsiteId", websiteId);

            return PopulateWebsiteModel(Commands.RetrieveDataRow(sqlCommand));
         }
      }

      public static void Update(Models.Website website)
      {
         using (SqlCommand sqlCommand = new SqlCommand("update [dbo].[Website] set [WebsiteId]=@WebsiteId,[SiteHitCount]=@SiteHitCount,[SiteLastHit]=@SiteLastHit where [WebsiteId]=@WebsiteId;"))
         {
            sqlCommand.Parameters.AddWithValue("@WebsiteId", website.WebsiteId);
            sqlCommand.Parameters.AddWithValue("@SiteHitCount", website.SiteHitCount);
            sqlCommand.Parameters.AddWithValue("@SiteLastHit", website.SiteLastHit);

            Commands.ExecuteNonQuery(sqlCommand);
         }
      }

      private static Models.Website? PopulateWebsiteModel(DataRow dataRow)
      {
         if (dataRow != null)
         {
            return new Models.Website {
                                         WebsiteId = Convert.ToInt32(dataRow["WebsiteId"]), 
                                         SiteHitCount = Convert.ToInt32(dataRow["SiteHitCount"]), 
                                         SiteLastHit = Convert.ToDateTime(dataRow["SiteLastHit"]), 
                                      };
         }

         return null;
      }
   }
}
