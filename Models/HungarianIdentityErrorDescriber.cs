using Microsoft.AspNetCore.Identity;

namespace MadarfigyeloWeb.Models
{
    public class HungarianIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DefaultError() => new IdentityError { Code = nameof(DefaultError), Description = "Ismeretlen hiba történt." };
        public override IdentityError ConcurrencyFailure() => new IdentityError { Code = nameof(ConcurrencyFailure), Description = "Optimista konkurencia hiba, az objektum módosult." };
        public override IdentityError PasswordMismatch() => new IdentityError { Code = nameof(PasswordMismatch), Description = "Helytelen jelszó." };
        public override IdentityError InvalidToken() => new IdentityError { Code = nameof(InvalidToken), Description = "Érvénytelen token." };
        public override IdentityError LoginAlreadyAssociated() => new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = "Már létezik felhasználó ezzel a bejelentkezéssel." };
        public override IdentityError InvalidUserName(string? userName) => new IdentityError { Code = nameof(InvalidUserName), Description = $"A felhasználónév '{userName}' érvénytelen, csak betűket vagy számokat tartalmazhat." };
        public override IdentityError InvalidEmail(string? email) => new IdentityError { Code = nameof(InvalidEmail), Description = $"Az email cím '{email}' érvénytelen." };
        public override IdentityError DuplicateUserName(string userName) => new IdentityError { Code = nameof(DuplicateUserName), Description = $"A felhasználónév '{userName}' már foglalt." };
        public override IdentityError DuplicateEmail(string email) => new IdentityError { Code = nameof(DuplicateEmail), Description = $"Az email cím '{email}' már foglalt." };
        public override IdentityError InvalidRoleName(string? role) => new IdentityError { Code = nameof(InvalidRoleName), Description = $"A szerep neve '{role}' érvénytelen." };
        public override IdentityError DuplicateRoleName(string role) => new IdentityError { Code = nameof(DuplicateRoleName), Description = $"A szerep neve '{role}' már foglalt." };
        public override IdentityError UserAlreadyHasPassword() => new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = "A felhasználónak már van jelszava beállítva." };
        public override IdentityError UserLockoutNotEnabled() => new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = "A zárolás nincs engedélyezve ennél a felhasználónál." };
        public override IdentityError UserAlreadyInRole(string role) => new IdentityError { Code = nameof(UserAlreadyInRole), Description = $"A felhasználó már tagja a '{role}' szerepnek." };
        public override IdentityError UserNotInRole(string role) => new IdentityError { Code = nameof(UserNotInRole), Description = $"A felhasználó nem tagja a '{role}' szerepnek." };
        public override IdentityError PasswordTooShort(int length) => new IdentityError { Code = nameof(PasswordTooShort), Description = $"A jelszónak legalább {length} karakter hosszúnak kell lennie." };
        public override IdentityError PasswordRequiresNonAlphanumeric() => new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "A jelszónak tartalmaznia kell legalább egy nem alfanumerikus karaktert." };
        public override IdentityError PasswordRequiresDigit() => new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "A jelszónak tartalmaznia kell legalább egy számot ('0'-'9')." };
        public override IdentityError PasswordRequiresLower() => new IdentityError { Code = nameof(PasswordRequiresLower), Description = "A jelszónak tartalmaznia kell legalább egy kisbetűt ('a'-'z')." };
        public override IdentityError PasswordRequiresUpper() => new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "A jelszónak tartalmaznia kell legalább egy nagybetűt ('A'-'Z')." };
    }
}
