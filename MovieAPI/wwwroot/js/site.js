$(document).ready(function(){
    getMovies();
})

$(document).on("submit", "#movieForm", function(e){
    e.preventDefault();
    var movieName = $('#movieForm').serializeArray()
    var movieString = movieName[0].value;
    var url = "https://api.themoviedb.org/3/search/movie?api_key=b25b8be5d63cef42f5507a812752e2cf&query=" + movieName[0].value;
    $.get(url, function(ApiResponse){
        var  movieObj = {"movie": [ApiResponse["results"][0]["title"], ApiResponse["results"][0]["release_date"], ApiResponse["results"][0]["vote_average"].toString()]};
        console.log(movieObj);
        $.ajax(
        {
            url: '/addMovie',
            type: 'POST',
            data: movieObj,
            success: function (data) {
                displayMovies(data);
            }
        });
    })
})

function findMovies(movie){
    var movieInfo = $('form').serializeArray();
}

function getMovies(title){
    $("#notes").text("");
    $.get("/getMovies", function(movies){
        for( movie of movies){
            var movieArr = [];
            movieArr.push(movie["title"], movie["releaseDate"], movie["rating"]);
            displayMovies(movieArr);
        }
    })
}

function displayMovies(movie){
    $("#movieTable").prepend(`
        <tr>
            <td>${movie[0]}</td>
            <td>Rating: ${movie[2]}</td>
            <td>Released ${movie[1]}</td>
        </tr>
    `);
}