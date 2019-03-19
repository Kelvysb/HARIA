
var gulp = require('gulp');
var sass = require('gulp-sass');
var exec = require('child_process').exec;

gulp.task('build', function(done) {    
    exec('npm start', function (err, stdout, stderr) {
        console.log(stdout);
        console.log(stderr);
      });
    done();
})

gulp.task('sass', function(){
    return gulp.src('app/sass/**/*.scss')
        .pipe(sass())
        .pipe(gulp.dest('app/css'));
});

gulp.task('watch', function (done){    
    gulp.watch('app/sass/**/*.scss', gulp.series('sass')); 
    done();
});

gulp.task('start', function(done) {
    gulp.series('build', 'sass', 'watch')(done);
})