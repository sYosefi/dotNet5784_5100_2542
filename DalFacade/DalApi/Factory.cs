using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using static DalApi.Config;//מאפשר שימוש במתודות סטטיות של מחלקה כאילו היו גלובליות

namespace DalApi
{

    public static class Factory
    {
        public static IDal Get
        {
            get
            {
                //ניגש לשדה s_dalName של מחלקת Config שהכנו ונקבל ממנו את שם האלמנט שמכיל את שם המחלקה שנרצה לממש
                string dalType = s_dalName ?? throw new DalConfigException($"DAL name is not extracted from the configuration");
                DalImplementation dal = s_dalPackages[dalType] ?? throw new DalConfigException($"Package for {dalType} is not found in packages list in dal-config.xml");

                //תטען את המודל (קובץ) * dll הנבחר
                try { Assembly.Load(dal.Package ?? throw new DalConfigException($"Package {dal.Package} is null")); }
                catch (Exception ex) { throw new DalConfigException($"Failed to load {dal.Package}.dll package", ex); }
                //מחזיר reflection(מטא-דטא) של הטיפוס Dal.DalList ממוֹדוּל DalList.dll או של הטיפוס Dal.DalXml ממוֹדוּל DalXml.dll
                Type type = Type.GetType($"{dal.Namespace}.{dal.Class}, {dal.Package}") ??
                    throw new DalConfigException($"Class {dal.Namespace}.{dal.Class} was not found in {dal.Package}.dll");
               	//הזימון type.GetProperty מחזיר מטא דטא של התכונה "Instance" של המחלקה DalXml / DalList שחייבת להיות סטטית ועם הרשאה public
               	//הזימון של(GetValue(null על המטא דטא של התכונה "Instance" מחזיר ערך של התכונה הסטטית הזו(שזהו אמור להיות המופע [היחיד] של מחלקת הסינגלטון DalList/ DalXml שלנו)
                return type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) as IDal ??
                    throw new DalConfigException($"Class {dal.Class} is not a singleton or wrong property name for Instance");
            }
        }
    }

}
