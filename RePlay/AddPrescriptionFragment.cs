using Android.App;
using Android.OS;
using Android.Content;
using Android.Widget;

namespace RePlay
{
    public class AddPrescriptionFragment : DialogFragment
    {
        //Create class properties
        protected EditText NameEditText;
        protected EditText DescriptionEditText;
        protected EditText PriceEditText;
        protected EditText AddCategoryEditText;
        protected Spinner CategorySpinner;
        protected LinearLayout CategoryLayout;
        protected CheckBox CategoryCheckBox;
        protected Button CategoryButton;

        //Create the string that will hold the value
        //Of the category drop down selected item
        protected string SelectedCategory = "";

        /// <summary>
        /// Method that creates and returns and instance of this dialog
        /// </summary>
        /// <returns></returns>
        public static AddPrescriptionFragment NewInstance()
        {
            var dialogFragment = new AddPrescriptionFragment();
            return dialogFragment;
        }


        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Begin building a new dialog.
            var builder = new AlertDialog.Builder(Activity);

            //Get the layout inflater
            var inflater = Activity.LayoutInflater;

            //Inflate the layout for this dialog
            var dialogView = inflater.Inflate(Resource.Layout.AddPrescription, null);

            if (dialogView != null)
            {
                var cancelButton = dialogView.FindViewById<Button>(Resource.Id.cancelButton);
                cancelButton.Click += (sender, args) =>
                {
                    Dismiss();
                };
                //Initialize the properties
//                NameEditText = dialogView.FindViewById<EditText>(Resource.Id.editTextName);
//                DescriptionEditText = dialogView.FindViewById<EditText>(Resource.Id.editTextDescription);
//                PriceEditText = dialogView.FindViewById<EditText>(Resource.Id.editTextPrice);
//                AddCategoryEditText = dialogView.FindViewById<EditText>(Resource.Id.editTextAddCategory);
//                CategorySpinner = dialogView.FindViewById<Spinner>(Resource.Id.spinnerCategory);

//                CategoryLayout = dialogView.FindViewById<LinearLayout>(Resource.Id.linearLayoutCategorySection);
                //Hide this section for now
//                CategoryLayout.Visibility = ViewStates.Invisible;
//                CategoryCheckBox = dialogView.FindViewById<CheckBox>(Resource.Id.checkBoxAddCategory);

//                CategoryCheckBox.Click += (sender, args) =>
//                {
//                    //If checked, show the Category section otherwise hide
//                    CategoryLayout.Visibility =
//                        CategoryCheckBox.Checked ? ViewStates.Visible : ViewStates.Invisible;
//                };

//                CategoryButton = dialogView.FindViewById<Button>(Resource.Id.buttonCategory);
//                CategoryButton.Click += (sender, args) =>
//                {
//                    var category = AddCategoryEditText.Text.ToString();
//
//                    //insert new category into the database
//                    if (category.Trim().Length <= 0)
//                    {
//                        Toast.MakeText(Activity, "Please enter category name", ToastLength.Short);
//                    }
//                    else
//                    {
//                        var newCategory = new ServiceCategory { Name = category };
//                        //Call the Category Manager to save the category
//                        CategoryManager.SaveCategory(newCategory);
//                        AddCategoryEditText.Text = "";
//                        //Now load call the method that loads the Spinner
//                        //So the Category you just added will be an available choice
//                        //in the drop down
//                        LoadSpinnerData();
//                    }
//                };
                //populate Spinner with data from database
                //Good candidate for async
                //LoadSpinnerData();

                //Set default selection for the spinner
                //CategorySpinner.SetSelection(0);
                //SelectedCategory = CategorySpinner.SelectedItem.ToString();

                //Set on item selected listener for the spinner
//                CategorySpinner.ItemSelected += spinner_ItemSelected;

                builder.SetView(dialogView);
            }

            //Create the builder 
            var dialog = builder.Create();

            //Now return the constructed dialog to the calling activity
            return dialog;
        }

        private void HandlePositiveButtonClick(object sender, DialogClickEventArgs e)
        {
            var dialog = (AlertDialog)sender;
            dialog.Dismiss();
        }

        private void HandleNegativeButtonClick(object sender, DialogClickEventArgs e)
        {
            var dialog = (AlertDialog)sender;
            dialog.Dismiss();
        }
    }
}