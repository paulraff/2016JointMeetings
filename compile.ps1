Param
(
    [Parameter(Mandatory=$True)]
    [string] $file
)

$dvi_file = $file.Replace(".tex", ".dvi")
$ps_file = $file.Replace(".tex", ".ps")

& latex $file
& dvips -Ppdf $dvi_file
& ps2pdf $ps_file
