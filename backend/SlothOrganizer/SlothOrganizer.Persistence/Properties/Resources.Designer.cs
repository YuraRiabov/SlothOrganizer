﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SlothOrganizer.Persistence.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SlothOrganizer.Persistence.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE FROM TaskCompletions
        ///WHERE Id=@Id.
        /// </summary>
        internal static string DeleteTaskCompletion {
            get {
                return ResourceManager.GetString("DeleteTaskCompletion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE FROM TaskCompletions
        ///WHERE TaskId = @TaskId AND [End] &gt; @RepeatingEnd.
        /// </summary>
        internal static string DeleteTaskCompletions {
            get {
                return ResourceManager.GetString("DeleteTaskCompletions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE FROM Users WHERE Id = @Id.
        /// </summary>
        internal static string DeleteUser {
            get {
                return ResourceManager.GetString("DeleteUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM Dashboards 
        ///WHERE UserId = @UserId.
        /// </summary>
        internal static string GetAllDashboards {
            get {
                return ResourceManager.GetString("GetAllDashboards", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT *
        ///FROM Tasks as t
        ///INNER JOIN TaskCompletions as tc
        ///ON t.Id = tc.TaskId
        ///WHERE t.DashboardId = @DashboardId.
        /// </summary>
        internal static string GetAllTasks {
            get {
                return ResourceManager.GetString("GetAllTasks", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT TOP(1) * FROM Users WHERE Id = @Id.
        /// </summary>
        internal static string GetByUserId {
            get {
                return ResourceManager.GetString("GetByUserId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM RefreshTokens 
        ///WHERE EXISTS (
        ///	SELECT * FROM Users
        ///	WHERE Users.Id = RefreshTokens.UserId
        ///	AND Users.Email = @UserEmail
        ///).
        /// </summary>
        internal static string GetRefreshTokenByUserEmail {
            get {
                return ResourceManager.GetString("GetRefreshTokenByUserEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT *
        ///FROM TaskCompletions
        ///WHERE TaskId = @TaskId
        ///.
        /// </summary>
        internal static string GetTaskCompletionsByTask {
            get {
                return ResourceManager.GetString("GetTaskCompletionsByTask", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT *
        ///FROM TaskCompletions
        ///WHERE TaskId = @TaskId
        ///.
        /// </summary>
        internal static string GetTaskCompletionsByTask1 {
            get {
                return ResourceManager.GetString("GetTaskCompletionsByTask1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT TOP(1) * FROM Users WHERE Email = @Email.
        /// </summary>
        internal static string GetUserByEmail {
            get {
                return ResourceManager.GetString("GetUserByEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT TOP(1) * FROM Users
        ///WHERE Email = @Email AND
        ///EXISTS (
        ///	SELECT * FROM VerificationCodes
        ///	WHERE Code = @Code AND
        ///	Users.Id = VerificationCodes.UserId
        ///).
        /// </summary>
        internal static string GetUserByEmailAndCode {
            get {
                return ResourceManager.GetString("GetUserByEmailAndCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM VerificationCodes 
        ///WHERE EXISTS (
        ///	SELECT * FROM Users
        ///	WHERE Users.Id = VerificationCodes.UserId
        ///	AND Users.Email = @UserEmail
        ///).
        /// </summary>
        internal static string GetVerificationCodeByUserEmail {
            get {
                return ResourceManager.GetString("GetVerificationCodeByUserEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO Dashboards (UserId, Title)
        ///VALUES (@UserId, @Title)
        ///
        ///SELECT CAST(SCOPE_IDENTITY() AS bigint).
        /// </summary>
        internal static string InsertDashboard {
            get {
                return ResourceManager.GetString("InsertDashboard", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO RefreshTokens(UserId, Token, ExpirationTime)
        ///VALUES (
        ///	(SELECT TOP(1) Id FROM Users WHERE Email = @UserEmail),
        ///	@Token,
        ///	@ExpirationTime
        ///).
        /// </summary>
        internal static string InsertRefreshToken {
            get {
                return ResourceManager.GetString("InsertRefreshToken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO Tasks (DashboardId, Title, Description)
        ///VALUES (@DashboardId, @Title, @Description)
        ///SELECT CAST(SCOPE_IDENTITY() AS bigint).
        /// </summary>
        internal static string InsertTask {
            get {
                return ResourceManager.GetString("InsertTask", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO TaskCompletions (TaskId, IsSuccessful, Start, [End], LastEdited)
        ///VALUES (@TaskId, @IsSuccessful, @Start, @End, @LastEdited)
        ///SELECT CAST(SCOPE_IDENTITY() AS bigint).
        /// </summary>
        internal static string InsertTaskCompletion {
            get {
                return ResourceManager.GetString("InsertTaskCompletion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO Users (FirstName, LastName, Email, Password, Salt, EmailVerified)
        ///VALUES (@FirstName, @LastName, @Email, @Password, @Salt, @EmailVerified)
        ///SELECT CAST(SCOPE_IDENTITY() as bigint).
        /// </summary>
        internal static string InsertUser {
            get {
                return ResourceManager.GetString("InsertUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO VerificationCodes(UserId, Code, ExpirationTime) 
        ///VALUES ((SELECT TOP(1) Id FROM Users WHERE Email = @UserEmail), @Code, @ExpirationTime)
        ///SELECT CAST(SCOPE_IDENTITY() as bigint).
        /// </summary>
        internal static string InsertVerificationCode {
            get {
                return ResourceManager.GetString("InsertVerificationCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM Users.
        /// </summary>
        internal static string SelectAllUsers {
            get {
                return ResourceManager.GetString("SelectAllUsers", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE TASKS
        ///SET
        ///	Title = @Title,
        ///	[Description] = @Description
        ///WHERE Id = @Id
        ///
        ///SELECT * 
        ///FROM Tasks as t
        ///INNER JOIN TaskCompletions AS tc
        ///ON t.Id = tc.TaskId
        ///WHERE t.Id = @Id.
        /// </summary>
        internal static string UpdateTask {
            get {
                return ResourceManager.GetString("UpdateTask", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE TaskCompletions
        ///SET  
        ///	IsSuccessful = @IsSuccessful,
        ///	[Start] = @Start,
        ///	[End] = @End,
        ///	LastEdited = @LastEdited
        ///WHERE Id=@Id
        ///SELECT TOP 1 * FROM TaskCompletions
        ///WHERE Id=@Id.
        /// </summary>
        internal static string UpdateTaskCompletion {
            get {
                return ResourceManager.GetString("UpdateTaskCompletion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE Users SET 
        ///	FirstName = @FirstName, 
        ///	LastName = @LastName, 
        ///	Email = @Email,
        ///	Password = @Password,
        ///	Salt = @Salt,
        ///	EmailVerified = @EmailVerified,
        ///	AvatarUrl = @AvatarUrl
        ///WHERE Id = @Id.
        /// </summary>
        internal static string UpdateUser {
            get {
                return ResourceManager.GetString("UpdateUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE Users SET 
        ///	AvatarUrl = @AvatarUrl
        ///WHERE Id = @Id
        ///
        ///SELECT * FROM Users WHERE Id = @Id.
        /// </summary>
        internal static string UpdateUserAvatar {
            get {
                return ResourceManager.GetString("UpdateUserAvatar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE Users SET 
        ///	FirstName = @FirstName
        ///WHERE Id = @Id.
        /// </summary>
        internal static string UpdateUserFirstName {
            get {
                return ResourceManager.GetString("UpdateUserFirstName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE Users SET 
        ///	LastName = @LastName
        ///WHERE Id = @Id.
        /// </summary>
        internal static string UpdateUserLastName {
            get {
                return ResourceManager.GetString("UpdateUserLastName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE Users 
        ///SET EmailVerified = 1
        ///WHERE Email = @Email AND EXISTS 
        ///	(SELECT * FROM VerificationCodes AS vc WHERE 
        ///		vc.UserId = Users.Id AND
        ///		vc.Code = @Code AND
        ///		vc.ExpirationTime &gt; SYSDATETIMEOFFSET())
        ///IF @@ROWCOUNT &gt; 0
        ///	SELECT TOP(1) * FROM Users WHERE Email = @Email.
        /// </summary>
        internal static string VerifyEmailByCode {
            get {
                return ResourceManager.GetString("VerifyEmailByCode", resourceCulture);
            }
        }
    }
}
