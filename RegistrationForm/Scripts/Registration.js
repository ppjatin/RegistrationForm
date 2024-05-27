$(document).ready(function () {

    function getRegisterdetail() { 
    $.ajax({
        url: '/UserRegistration/Details',
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            $('#userTable tbody').empty();
            $.each(data, function (index, user) {
                var row = '<tr>' +
                    '<td>' + user.Name + '</td>' +
                    '<td>' + user.Email + '</td>' +
                    '<td>' + user.Phone + '</td>' +
                    '<td>' + user.Address + '</td>' +
                    '<td>' + user.StateName + '</td>' +
                    '<td>' + user.CityName + '</td>' +
                    '<td>' +
                    '<button class="editBtn" data-id="' + user.Id + '" data-name="' + user.Name + '" data-email="' + user.Email + '" data-phone="' + user.Phone + '" data-address="' + user.Address + '" data-state="' + user.StateName + '" data-city="' + user.CityName + '"> Edit</button> ' +
                    '<button class="deleteBtn" data-id="' + user.Id + '">Delete</button>' +
                    '</td>' +
                    '</tr>';
                $('#userTable tbody').append(row);
            });
        },
        error: function (error) {
            alert('Error fetching user details: ' + error.responseText);
        }
    });
}
    getRegisterdetail();

    function clear() {

        $('#nameError').hide();
        $('#emailError').hide();
        $('#phoneError').hide();
        $('#stateError').hide();
        $('#cityError').hide();
    }


    $('#userTable').on('click', '.deleteBtn', function () {
        var id = $(this).data('id');

        // Display a confirmation dialog using SweetAlert2
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                // User confirmed, proceed with deletion
                $.ajax({
                    url: '/UserRegistration/DeleteUserRegistration',
                    type: "POST",
                    dataType: 'json',
                    data: { id: id },
                    success: function (response) {
                        toastr.success('User Deleted successfully!');
                        getRegisterdetail();
                    }
                });
            }
        });

        // This line should be removed from here
        // getRegisterdetail();
    });




    $('#userTable').on('click', '.editBtn', function () {
        clear();
        $('#submitBtn').text('Update');
        var userId = $(this).data('id');
        var name = $(this).data('name');
        var email = $(this).data('email');
        var phone = $(this).data('phone');
        var address = $(this).data('address');
        var StateName = $(this).data('state');
        var CityName = $(this).data('city');
        $('#name').val(name);
        $('#email').val(email);
        $('#phone').val(phone);
        $('#address').val(address);
        $('#id').val(userId);

        var option = $('#state option').filter(function () {
            return $(this).text() === StateName;
           
        });

        if (option.length) {
            $('#state').val(option.val()).trigger('change');
           
           
            
        } else {
            alert('State not found in the dropdown');
        }

      
            setTimeout(function () {
                var cityOption = $('#city option').filter(function () {
                    return $(this).text() === CityName;
                });

                if (cityOption.length) {
                    $('#city').val(cityOption.val()).trigger('change'); 
                } else {
                    alert('City not found in the dropdown');
                }
            }, 100); 
        $('#formModal').css('display', 'block');
    });

    function changeStateDropdown() {
        $('#state').change(function () {
            var id = $(this).val();
            $('#city').empty();
            $('#city').append('<option>Select City</option>');
            if (id) {
                $.ajax({
                    url: '/UserRegistration/GetCity',
                    type: "GET",
                    dataType: 'json',
                    data: { id: id },
                    success: function (data) {
                        $.each(data, function (index, item) {
                            $('#city').append('<option value="' + item.Id + '">' + item.CityName + '</option>');
                        });
                    }
                });
            }
        });
    }
    changeStateDropdown();

    function populateStateDropdown() {
        $.ajax({
            url: '/UserRegistration/GetSate',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var $stateDropdown = $('#state');
                $stateDropdown.empty(); 
                $stateDropdown.append('<option value="">Select State</option>'); 
                $.each(data, function (index, state) {
                    $stateDropdown.append('<option value="' + state.Value + '">' + state.Text + '</option>');
                });
            },
            error: function (xhr, status, error) {
                console.log("Error: " + error);
            }
        });
    }

   
    populateStateDropdown();



    
    $('#addBtn').click(function () {
        $('#formModal').css('display', 'block');
        clear();
    });

    
    $('.close, .cancelBtn').click(function () {
        $('#formModal').css('display', 'none');
    });

    
    $('#agree').change(function () {
        $('#submitBtn').prop('disabled', !$(this).is(':checked'));
    });

   
    $('#state').change(function () {
        var state = $(this).val();
        $('#city').html('<option value="">Select City</option>'); 
        if (state === 'Gujarat') {
            $('#city').append('<option value="Surat">Surat</option><option value="Bardoli">Bardoli</option><option value="Baroda">Baroda</option>');
        } else if (state === 'Maharashtra') {
            $('#city').append('<option value="Mumbai">Mumbai</option><option value="Pune">Pune</option>');
        }
    });

   
    function isValidEmail(email) {
        var pattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return pattern.test(email);
    }

    
   


    function isValidPhone(phone) {
       
        var pattern = /^(?:\+?91|0)?[789]\d{9}$/;
        return pattern.test(phone);
    }

   
    function validateForm() {
        var isValid = true;

       
        var name = $('#name').val().trim();
        if (name === '') {
            $('#nameError').text('Name is required').show();
            isValid = false;
        } else {
            $('#nameError').hide();
        }

        
        var email = $('#email').val().trim();
        if (email === '' || !isValidEmail(email)) {
            $('#emailError').text('Valid email is required').show();
            isValid = false;
        } else {
            $('#emailError').hide();
        }

        
        var phone = $('#phone').val().trim();
        if (phone !== '' && !isValidPhone(phone)) {
            $('#phoneError').text('Please Enter Valid phone number').show();
            isValid = false;
        } else {
            $('#phoneError').hide();
        }

       
        var state = $('#state').val();
        if (state === '') {
            $('#stateError').text('State is required').show();
            isValid = false;
        } else {
            $('#stateError').hide();
        }

        
        var city = $('#city').val();
        if (city === '') {
            $('#cityError').text('City is required').show();
            isValid = false;
        } else {
            $('#cityError').hide();
        }

        
        var agree = $('#agree').prop('checked');
        if (!agree) {
            $('#agreeError').text('You must agree to continue').show();
            isValid = false;
        } else {
            $('#agreeError').hide();
        }

        return isValid;
    }

   
    $('#submitBtn').on('click', function (e) {
        e.preventDefault();

        if (validateForm()) {
            var formData = {
                Id:$('#id').val(),
                Name: $('#name').val(),
                Email: $('#email').val(),
                Phone: $('#phone').val(),
                Address: $('#address').val(),
                StateId: $('#state').val(),
                CityId: $('#city').val()
            };
            var id = $('#id').val();


            $.ajax({
                url: '/UserRegistration/AddEditUserRegistrationDetail',
                type: 'POST',
                data: JSON.stringify(formData),
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                   
                    if (id == 0) {
                        getRegisterdetail();
                        toastr.success('User registered successfully!');
                    }
                    else {
                        getRegisterdetail();
                        toastr.success('User Updated successfully!');
                    }
                    $('#formModal').hide();
                    $('#dataForm')[0].reset();
                },
                error: function (error) {
                    alert('Error: ' + error.responseText);
                }
            });

            getRegisterdetail();
        }
    });

    
});
